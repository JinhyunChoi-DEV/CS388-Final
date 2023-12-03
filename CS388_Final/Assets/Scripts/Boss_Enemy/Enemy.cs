using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Pool;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public EnemyBullet bulletPrefab;
    public Transform firePosition;
    public float BulletSpeed;
    public float FireTime;
    public float MinDistanceToPlayer = 8;
    public NavMeshAgent agent;

    public LayerMask obstacleLayer;
    public Slider hpbar;
    private Vector2 DirToPlayer;
    private float CurHp = 100f;
    private float MaxHP = 100f;

    private GameObject player;
    private ObjectPool<EnemyBullet> pool;
    private bool isFirstShoot = true;
    private bool canFire;
    private Coroutine fireCoroutine = null;

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
        DirToPlayer = (player.transform.position - transform.position);

        float deg = Mathf.Atan2(DirToPlayer.y, DirToPlayer.x) * Mathf.Rad2Deg;
        Debug.Log(deg);

        hpbar.value = CurHp / MaxHP;

        float distanceToPlayer = DirToPlayer.magnitude;

        if (distanceToPlayer > MinDistanceToPlayer)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            if (!IsObstacleBetweenEnemyAndPlayer())
            {
                UpdateFire();
                agent.ResetPath();
            }
            else
            {
                agent.SetDestination(player.transform.position);
            }
        }

        if (CurHp <= 0)
        {
            //TODO delete corutine
            Destroy(gameObject);
        }

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


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            CurHp -= 20;
        }
    }
}
