using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Player;
    public GameObject bullet;
    private Vector2 DirToPlayer;
    private Vector2 Speed = new Vector2(2f,2f);
    private float MinDistanceToPlayer = 3;
    private float timer = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DirToPlayer =( Player.gameObject.transform.position - gameObject.transform.position);
        if (DirToPlayer.magnitude > MinDistanceToPlayer)
        {
            GetComponent<Rigidbody2D>().velocity = Speed * DirToPlayer.normalized;
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >2f)
            {
                GenerateBullet();
                timer = 0;
            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
        }
    }

    void GenerateBullet()
    {
        GameObject bullet_ = Instantiate(bullet, gameObject.transform);
        bullet_.transform.rotation = Quaternion.FromToRotation(Vector3.up, DirToPlayer);
        bullet_.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 10f) * DirToPlayer.normalized;
    }
}
