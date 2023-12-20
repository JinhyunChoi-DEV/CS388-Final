/*
* Name: Bullet
* Author: Jinhyun Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private ObjectPool<Bullet> pool;
    private float liveTime = 10.0f;
    private bool isRelease;
    private float damage = 0;

    private void Start()
    {
        isRelease = false;
        StartCoroutine(DeactiveAfterTime());
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void Fire(Vector2 dir, float speed)
    {
        isRelease = false;
        rb.velocity = dir * speed;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
            return;

        if (other.gameObject.tag == "Bullet")
            return;

        if (other.gameObject.tag == "Trigger")
            return;

        if (other.gameObject.tag == "Boss")
        {
            var boss = other.GetComponent<Boss>();
            boss.ApplyDamage(damage);
        }

        if (other.gameObject.tag == "Enemy")
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy.IsDead)
                return;

            enemy.ApplyDamage(damage);
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
