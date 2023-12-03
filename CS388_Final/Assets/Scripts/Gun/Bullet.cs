using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    private ObjectPool<Bullet> pool;
    private float liveTime = 10.0f;
    private bool isRelease;

    private void Start()
    {
        isRelease = false;
        StartCoroutine(DeactiveAfterTime());
    }

    public void Fire(Vector2 dir, float speed)
    {
        isRelease = false;
        rigidbody.velocity = dir * speed;
    }

    public void SetPool(ObjectPool<Bullet> pool)
    {
        this.pool = pool;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

        }
        else
        {
            Release();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {

        }
        else
        {
            Release();
        }
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
