using UnityEngine;

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

    void Start()
    {
        currentBulletCount = MaxBulletCount;
        isInfiniteBullet = TotalBullets == -1;
        fireTimer = 0.0f;

        waitFireDelay = false;
        waitReload = false;
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

        //TODO Use ObjectPool
        var angle = aim.WeaponAngle;
        Quaternion angleQ = Quaternion.Euler(0, 0, angle);
        var newBullet = Instantiate(Bullet, firePosition.position, angleQ);

        var aimAngle = aim.Angle;
        var dir = new Vector2(Mathf.Sin(aimAngle * Mathf.Deg2Rad), Mathf.Cos(aimAngle * Mathf.Deg2Rad)).normalized;
        newBullet.gameObject.SetActive(true);
        newBullet.Fire(dir, BulletSpeed);
        waitFireDelay = true;
        currentBulletCount -= 1;
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
