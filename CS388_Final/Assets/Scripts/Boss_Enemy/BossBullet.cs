using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BossBullet : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool IgnoreRelease;
    private ObjectPool<BossBullet> pool;
    private float liveTime = 10.0f;
    private bool isRelease;

    public void Fire(Vector2 dir, float speed)
    {
        isRelease = false;
        rb.velocity = dir * speed;
    }

    public void SetPool(ObjectPool<BossBullet> pool)
    {
        this.pool = pool;
    }

    void Start()
    {
        isRelease = false;
        StartCoroutine(DeactiveAfterTime());
    }

    // Update is called once per frame
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

    public void Release()
    {
        if (!isRelease)
        {
            isRelease = true;
            pool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
            return;

        if (other.gameObject.tag == "Bullet")
            return;

        if (other.gameObject.tag == "Trigger")
            return;

        if (other.gameObject.tag == "Player")
        {
            var data = other.GetComponent<PlayerData>();
            if (data.IgnoreDamageCollision)
                return;

            data.ApplyDamage();

            if (IgnoreRelease)
                return;
        }

        Release();
    }
}
