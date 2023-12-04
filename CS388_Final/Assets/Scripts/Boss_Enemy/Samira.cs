using UnityEngine;
using UnityEngine.Pool;

public class Samira : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private BossBullet bulletPrefab;
    public float speed;

    private ObjectPool<BossBullet> pool;

    public void Fire()
    {
        float angle = 12f;

        for (int i = 0; i < 30; i++)
        {
            Vector2 direction = new Vector2(Mathf.Sin(angle * i * Mathf.Deg2Rad), Mathf.Cos(angle * i * Mathf.Deg2Rad));
            var objectAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var bullet = pool.Get();
            bullet.transform.rotation = Quaternion.Euler(0, 0, objectAngle);
            bullet.Fire(direction, speed);
        }
    }

    private void Start()
    {
        bulletPrefab.gameObject.SetActive(false);
        pool = new ObjectPool<BossBullet>(CreateBullet, Create, Return, Delete, true, 30, 50);
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
