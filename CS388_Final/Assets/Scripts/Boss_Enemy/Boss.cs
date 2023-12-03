using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject Bullet;

    private Vector2 DirToPlayer;
    private Vector2 Speed = new Vector2(2f, 2f);
    private float Talon_w_time =0.3f;
    void Start()
    {
        DirToPlayer = (Player.gameObject.transform.position - gameObject.transform.position);
        Talon_W();
    }

    // Update is called once per frame
    void Update()
    {
        DirToPlayer = (Player.gameObject.transform.position - gameObject.transform.position);
    }

    void Zeri_Q()
    {
        GameObject bullet_ = Instantiate(Bullet, gameObject.transform);
        bullet_.transform.rotation = Quaternion.FromToRotation(Vector3.up, DirToPlayer);
        bullet_.GetComponent<Rigidbody2D>().velocity = DirToPlayer.normalized * new Vector2(10f, 10f);
    }

    void Talon_W()
    {
        float angle = 15f;
        for (int i = 0; i < 3; i++)
        {
            Vector2 originalDirection = DirToPlayer.normalized;
            Vector2 direction = RotateVector2D(originalDirection, (angle * i) - 15f);

            GameObject bullet_ = Instantiate(Bullet, gameObject.transform.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet_.GetComponent<Rigidbody2D>();
            bulletRigidbody.velocity = direction.normalized * new Vector2(20f, 20f);

           StartCoroutine(ReturnBulletAfterDelay(bulletRigidbody, Talon_w_time));
 
        }
    }

    void Samira_R()
    {
        float angle = 12f;
        for (int i = 0; i < 30; i++)
        {
            Vector2 direction = new Vector2(Mathf.Cos(angle * i * Mathf.Deg2Rad), Mathf.Sin(angle *i * Mathf.Deg2Rad));
            GameObject bullet_ = Instantiate(Bullet, gameObject.transform);
            bullet_.transform.rotation = Quaternion.FromToRotation(Vector3.up, DirToPlayer);
            bullet_.GetComponent<Rigidbody2D>().velocity = direction * new Vector2(10f,10f);
        }
    }

    Vector2 RotateVector2D(Vector2 vector, float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 rotatedVector = rotation * vector;

        return rotatedVector;
    }
    System.Collections.IEnumerator ReturnBulletAfterDelay(Rigidbody2D bulletRigidbody, float delay)
    {
        yield return new WaitForSeconds(delay);
        bulletRigidbody.velocity = -bulletRigidbody.velocity;

        StartCoroutine(DestroyBulletAfterDelay(bulletRigidbody, Talon_w_time)); 
    }
    System.Collections.IEnumerator DestroyBulletAfterDelay(Rigidbody2D bulletRigidbody, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(bulletRigidbody.gameObject);
    }
}
