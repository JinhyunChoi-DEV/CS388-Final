using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Pool;

public class Talon : MonoBehaviour
{
    [SerializeField] private Boss boss;
    [SerializeField] private BossBullet bulletPrefab;
    public float Speed;
    public float ReturnDelay;

    private ObjectPool<BossBullet> pool;
    private Coroutine returnCoroutine = null;
    private Coroutine releaseCoroutine = null;

    private List<BossBullet> firedBullet = new List<BossBullet>();

    public void ClearFlag()
    {
        if (returnCoroutine != null)
            StopCoroutine(returnCoroutine);

        if (releaseCoroutine != null)
            StopCoroutine(releaseCoroutine);
    }

    public void Fire(Vector2 DirToPlayer)
    {
        firedBullet.Clear();
        float angle = 15f;
        for (int i = 0; i < 3; i++)
        {
            Vector2 originalDirection = DirToPlayer.normalized;
            Vector2 direction = RotateVector2D(originalDirection, (angle * i) - 15f);

            var bullet = pool.Get();
            bullet.Fire(direction.normalized, Speed);
            firedBullet.Add(bullet);
        }

        if (returnCoroutine != null)
            StopCoroutine(returnCoroutine);
        returnCoroutine = StartCoroutine(ReturnBullets());
    }

    private void Start()
    {
        bulletPrefab.gameObject.SetActive(false);
        pool = new ObjectPool<BossBullet>(CreateBullet, Create, Return, Delete, true, 5, 10);
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

    Vector2 RotateVector2D(Vector2 vector, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 rotatedVector = rotation * vector;

        return rotatedVector;
    }

    IEnumerator ReturnBullets()
    {
        float timer = 0.0f;
        while (timer < ReturnDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        returnCoroutine = null;
        foreach (var bullet in firedBullet)
            bullet.rb.velocity = -bullet.rb.velocity;

        if (releaseCoroutine != null)
            StopCoroutine(releaseCoroutine);
        releaseCoroutine = StartCoroutine(DestroyBulletAfterDelay());
    }

    IEnumerator DestroyBulletAfterDelay()
    {
        float timer = 0.0f;
        while (timer < ReturnDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        releaseCoroutine = null;
        foreach (var bullet in firedBullet)
            bullet.Release();
    }

}
