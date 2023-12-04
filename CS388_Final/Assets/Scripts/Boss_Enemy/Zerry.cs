using UnityEngine;
using UnityEngine.Pool;

public class Zerry : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private BossBullet bulletPrefab;
    public float speed;

    private ObjectPool<BossBullet> pool;

    public void Fire(Vector2 DirToPlayer)
    {
        var bullet = pool.Get();
        var angle = Mathf.Atan2(DirToPlayer.y, DirToPlayer.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.Fire(DirToPlayer.normalized, speed);
    }

    private void Start()
    {
        bulletPrefab.gameObject.SetActive(false);
        pool = new ObjectPool<BossBullet>(CreateBullet, Create, Return, Delete, true, 10, 15);
    }

    private BossBullet CreateBullet()
    {
        BossBullet newBullet = Instantiate(bulletPrefab, boss.bulletStartTransform.position, Quaternion.identity);
        newBullet.SetPool(pool);
        return newBullet;
    }

    private void Create(BossBullet bullet)
    {
        bullet.transform.position = boss.bulletStartTransform.position;
        bullet.transform.rotation = Quaternion.identity;
        bullet.gameObject.SetActive(true);
    }

    private void Return(BossBullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void Delete(BossBullet bullet)
    {
        Destroy(bullet.gameObject);
    }
}
