using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private ObjectPool<EnemyBullet> pool;
    private float liveTime = 10.0f;
    private bool isRelease;

    public void Fire(Vector2 dir, float speed)
    {
        isRelease = false;
        rb.velocity = dir * speed;
    }

    public void SetPool(ObjectPool<EnemyBullet> pool)
    {
        this.pool = pool;
    }

    void Start()
    {
        isRelease = false;
        StartCoroutine(DeactiveAfterTime());
    }

    void Update()
    {

    }

    IEnumerator DeactiveAfterTime()
    {
        float timer = 0;
        while (timer < liveTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Release();
    }

    private void Release()
    {
        if (!isRelease)
        {
            isRelease = true;
            pool.Release(this);
        }
    }
}
