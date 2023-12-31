/*
* Name: Gun
* Author: Jinhyun Choi
* Copyright � 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private PlayerAim aim;
    public PlayerWeapon.GunType Type;
    public GameObject Holder;
    public Bullet Bullet;
    public Transform firePosition;
    public int MaxBulletCount;
    public int TotalBullets;
    public float FireDelay;
    public float ReloadDelay;
    public float BulletSpeed;
    public float Damage;
    public float CameraShakeValue;
    public int CurrentBulletCount { get; private set; }
    public bool WaitReload { private set; get; }
    private bool isInfiniteBullet;
    private bool waitFireDelay;
    private bool canFire => CurrentBulletCount > 0 && WaitReload == false && state.State != State.Dodge && waitFireDelay != true;

    private ObjectPool<Bullet> pool;

    private Coroutine fireCoroutine = null;
    private Coroutine reloadCoroutine = null;

    public AudioClip Reload_Clip;
    public AudioClip Pistol_Clip;
    public AudioClip Rifle_Clip;
    public AudioClip Sniper_Clip;

    public void ClearFlag()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);

        if (reloadCoroutine != null)
            StopCoroutine(reloadCoroutine);

        waitFireDelay = false;
        WaitReload = false;
    }

    void Start()
    {
        CurrentBulletCount = MaxBulletCount;
        isInfiniteBullet = TotalBullets == -1;

        waitFireDelay = false;
        WaitReload = false;
        pool = new ObjectPool<Bullet>(CreateBullet, Create, Return, Delete, true, 100, 200);
    }

    void Update()
    {
        if (!PlayerData.IsAlive)
            return;

        if (PlayerInput.Instance.InputData.Fire && canFire)
            Fire();

        if(TotalBullets == -1)
        {
            if (CurrentBulletCount <= 0 || PlayerInput.Instance.InputData.Reload)
                Reload();
        }
        else
        {
            if ((CurrentBulletCount <= 0 || PlayerInput.Instance.InputData.Reload) && (TotalBullets > 0))
                Reload();
        }
    }

    void Fire()
    {
        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);

        fireCoroutine = StartCoroutine(FireUpdate());
        pool.Get();

        if (Type == PlayerWeapon.GunType.Pistol)
            SoundManager.instance.SFXPlay("Pistol", Pistol_Clip);
        else if (Type == PlayerWeapon.GunType.Rifle)
            SoundManager.instance.SFXPlay("Rifle", Rifle_Clip);
        else if (Type == PlayerWeapon.GunType.Sniper)
            SoundManager.instance.SFXPlay("Sniper", Sniper_Clip);

        CurrentBulletCount -= 1;
        UtilsClass.ShakeCamera(CameraShakeValue, 0.1f);
    }

    private Bullet CreateBullet()
    {
        var angle = aim.WeaponAngle;
        Quaternion angleQ = Quaternion.Euler(0, 0, angle);
        Bullet newBullet = Instantiate(Bullet, firePosition.position, angleQ);
        newBullet.SetPool(pool);
        return newBullet;
    }

    private void Create(Bullet bullet)
    {
        var angle = aim.WeaponAngle;
        var aimAngle = aim.Angle;
        var dir = new Vector2(Mathf.Sin(aimAngle * Mathf.Deg2Rad), Mathf.Cos(aimAngle * Mathf.Deg2Rad)).normalized;
        Quaternion angleQ = Quaternion.Euler(0, 0, angle);

        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = angleQ;
        bullet.gameObject.SetActive(true);
        bullet.Fire(dir, BulletSpeed);
        bullet.SetDamage(Damage);
    }

    private void Return(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void Delete(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    void Reload()
    {
        if (WaitReload)
            return;

        if (reloadCoroutine != null)
            StopCoroutine(reloadCoroutine);

        SoundManager.instance.SFXPlay("Reload", Reload_Clip);
        reloadCoroutine = StartCoroutine(ReloadBullet());
        WaitReload = true;
    }

    private void UpdateReload()
    {
        if (TotalBullets - MaxBulletCount < 0)
            return;

        if (CurrentBulletCount == 0)
        {
            CurrentBulletCount = MaxBulletCount;
            TotalBullets -= MaxBulletCount;
        }
        else
        {
            var needReloadBullet = MaxBulletCount - CurrentBulletCount;
            CurrentBulletCount = MaxBulletCount;
            TotalBullets -= needReloadBullet;
        }
    }

    IEnumerator FireUpdate()
    {
        waitFireDelay = true;

        yield return new WaitForSeconds(FireDelay);

        waitFireDelay = false;
        fireCoroutine = null;
    }

    IEnumerator ReloadBullet()
    {
        if (CurrentBulletCount == 0)
            CurrentBulletCount = 0;

        WaitReload = true;

        yield return new WaitForSeconds(ReloadDelay);

        if (isInfiniteBullet)
            CurrentBulletCount = MaxBulletCount;
        else
            UpdateReload();

        WaitReload = false;
        reloadCoroutine = null;
    }
}
