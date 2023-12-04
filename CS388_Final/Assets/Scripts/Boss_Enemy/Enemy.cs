using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    public EnemyBullet bulletPrefab;
    public Transform firePosition;
    public float BulletSpeed;
    public float FireTime;
    public float MinDistanceToPlayer = 8;
    public NavMeshAgent agent;
    public Transform targetTransform;
    public Transform bar;
    public LayerMask obstacleLayer;

    public bool IsMove { get; private set; }
    public bool IsDead { get; private set; }

    private Vector2 DirToPlayer;
    private float CurHp = 100f;
    private float MaxHP = 100f;

    private GameObject player;
    private ObjectPool<EnemyBullet> pool;
    private bool isFirstShoot = true;
    private bool canFire;
    private Coroutine fireCoroutine = null;

    public AudioSource Enemy_Audio;

    public AudioClip Damaged_Clip;
    public AudioClip Death_Clip;
    public AudioClip Fire_Clip;

    public void DestroyObject()
    {
        MapData.RemainEnemy -= 1;
        Destroy(gameObject);
    }

    void Start()
    {
        player = GameObject.Find("Player");
        bulletPrefab.gameObject.SetActive(false);
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        canFire = true;
        isFirstShoot = true;
        pool = new ObjectPool<EnemyBullet>(CreateBullet, Create, Return, Delete, true, 10, 20);
    }

    void Update()
    {
        if (!PlayerData.IsAlive)
            return;

        if (IsDead==false && CurHp <= 0)
        {
            Enemy_SFX("Death", Death_Clip);
            IsDead = true;
            return;
        }

        DirToPlayer = (player.transform.position - transform.position);

        RotateTransform();

        float distanceToPlayer = DirToPlayer.magnitude;

        if (distanceToPlayer > MinDistanceToPlayer)
        {
            agent.SetDestination(player.transform.position);
            IsMove = true;
        }
        else
        {
            if (!IsObstacleBetweenEnemyAndPlayer())
            {
                UpdateFire();
                agent.ResetPath();
                IsMove = false;
            }
            else
            {
                agent.SetDestination(player.transform.position);
                IsMove = true;
            }
        }

    }

    private void UpdateHealth()
    {
        var t = CurHp / MaxHP;
        var newSize = Mathf.Lerp(0, 1, t);

        bar.localScale = new Vector3(newSize, 1);
    }

    private void RotateTransform()
    {
        float deg = Mathf.Atan2(DirToPlayer.y, DirToPlayer.x) * Mathf.Rad2Deg;
        var newAngle = Mathf.Abs(deg) <= 90 ? 0 : 180.0f;
        targetTransform.transform.localRotation = Quaternion.Euler(0, newAngle, 0);
    }

    void UpdateFire()
    {
        if (!canFire)
            return;

        if (isFirstShoot)
        {
            Fire();
            isFirstShoot = false;
        }
        else
        {
            fireCoroutine = StartCoroutine(FireHandling());
        }
    }

    IEnumerator FireHandling()
    {
        canFire = false;

        yield return new WaitForSeconds(FireTime);

        Fire();
        canFire = true;
    }

    void Fire()
    {
        Enemy_SFX("Fire", Fire_Clip);
        pool.Get();
    }

    bool IsObstacleBetweenEnemyAndPlayer()
    {
        float distanceToPlayer = DirToPlayer.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, distanceToPlayer, obstacleLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private EnemyBullet CreateBullet()
    {
        EnemyBullet newBullet = Instantiate(bulletPrefab, firePosition.position, Quaternion.identity);
        newBullet.SetPool(pool);
        return newBullet;
    }

    private void Create(EnemyBullet bullet)
    {
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.SetActive(true);
        bullet.Fire(DirToPlayer.normalized, BulletSpeed);
    }

    private void Return(EnemyBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void Delete(EnemyBullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    public void ApplyDamage(float damage)
    {
        if (CurHp > 0)
        {
            Enemy_SFX("Damaged", Damaged_Clip);
            CurHp -= damage;
        }
        else
        {
            if (fireCoroutine != null)
                StopCoroutine(fireCoroutine);

            fireCoroutine = null;
            agent.velocity = Vector3.zero;
            Enemy_SFX("Death", Death_Clip);
        }
        UpdateHealth();
    }

    private void Enemy_SFX(string sfxName, AudioClip clip)
    {
        GameObject sound = new GameObject(sfxName + "Sound");
        Enemy_Audio = sound.AddComponent<AudioSource>();
        Enemy_Audio.clip = clip;
        Enemy_Audio.Play();

        Destroy(sound, clip.length);
    }
}
