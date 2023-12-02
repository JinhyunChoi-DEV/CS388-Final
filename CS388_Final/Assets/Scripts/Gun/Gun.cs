using UnityEngine;
using UnityEngine.Pool;

public class Gun : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private PlayerAim aim;
    public Bullet Bullet;
    public Transform firePosition;
    public int MaxBulletCount;
    public int TotalBullets;
    public float FireDelay;
    public float ReloadDelay;
    public float BulletSpeed;
    public float Damage;

    private int currentBulletCount;
    private float fireTimer;
    private float reloadTimer;
    private bool isInfiniteBullet;
    private bool waitFireDelay;
    private bool waitReload;
    private bool canFire => currentBulletCount > 0 && waitReload == false && state.State != State.Dodge;

    private ObjectPool<Bullet> pool;

    void Start()
    {
        currentBulletCount = MaxBulletCount;
        isInfiniteBullet = TotalBullets == -1;
        fireTimer = 0.0f;

        waitFireDelay = false;
        waitReload = false;
        pool = new ObjectPool<Bullet>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 500, 1000);
    }

    void Update()
    {
        UpdateDelayTime(ref waitFireDelay, ref fireTimer, FireDelay);
        UpdateDelayTime(ref waitReload, ref reloadTimer, ReloadDelay);

        if (PlayerInput.Instance.InputData.Fire && canFire)
            Fire();

        if (currentBulletCount <= 0 || PlayerInput.Instance.InputData.Reload)
            Reload();
    }

    void Fire()
    {
        if (waitFireDelay)
            return;

        pool.Get();

        waitFireDelay = true;
        currentBulletCount -= 1;
        UtilsClass.ShakeCamera(0.05f, 0.1f);
    }

    private Bullet CreateBullet()
    {
        var angle = aim.WeaponAngle;
        Quaternion angleQ = Quaternion.Euler(0, 0, angle);
        Bullet newBullet = Instantiate(Bullet, firePosition.position, angleQ);
        newBullet.SetPool(pool);
        return newBullet;
    }

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        var angle = aim.WeaponAngle;
        var aimAngle = aim.Angle;
        var dir = new Vector2(Mathf.Sin(aimAngle * Mathf.Deg2Rad), Mathf.Cos(aimAngle * Mathf.Deg2Rad)).normalized;
        Quaternion angleQ = Quaternion.Euler(0, 0, angle);

        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = angleQ;
        bullet.gameObject.SetActive(true);
        bullet.Fire(dir, BulletSpeed);
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    void Reload()
    {
        if (waitReload)
            return;

        if (isInfiniteBullet)
            currentBulletCount = MaxBulletCount;
        else
            UpdateReload();

        waitReload = true;
    }

    void UpdateDelayTime(ref bool checkFlag, ref float targetTimer, float totalTime)
    {
        if (checkFlag)
        {
            targetTimer += Time.deltaTime;
            if (targetTimer >= totalTime)
            {
                targetTimer = 0.0f;
                checkFlag = false;
            }
        }
    }

    private void UpdateReload()
    {
        //TODO: make warning panel;
        if (TotalBullets - MaxBulletCount < 0)
            return;

        currentBulletCount = MaxBulletCount;
        TotalBullets -= MaxBulletCount;
    }
}
