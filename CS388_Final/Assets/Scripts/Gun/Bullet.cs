using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidbody;


    public void Fire(Vector2 dir, float speed)
    {
        rigidbody.velocity = dir * speed;
    }

    //TODO Collision
}
