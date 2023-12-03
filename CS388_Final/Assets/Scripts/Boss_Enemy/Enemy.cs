using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public GameObject bullet;
    public LayerMask obstacleLayer;
    private NavMeshAgent agent;
    public Slider hpbar;
    private Vector2 DirToPlayer;
    private float MinDistanceToPlayer = 8;
    private float timer = 0;
    private float CurHp = 100f;
    private float MaxHP = 100f;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        DirToPlayer = (Player.transform.position - transform.position);

        hpbar.value = CurHp / MaxHP;

        float distanceToPlayer = DirToPlayer.magnitude;

        if (distanceToPlayer > MinDistanceToPlayer)
        {
            agent.SetDestination(Player.transform.position);
        }
        else
        {
            if (IsObstacleBetweenEnemyAndPlayer() == false)
            {
                timer += Time.deltaTime;
                if (timer > 2f)
                {
                    GenerateBullet();
                    timer = 0;
                }
                agent.ResetPath();
            }
            else
            {
                agent.SetDestination(Player.transform.position);
            }
        }

        if(CurHp <= 0)
        {
            Destroy(gameObject);
        }

    }

    void GenerateBullet()
    {
        GameObject bullet_ = Instantiate(bullet, gameObject.transform);
        bullet_.transform.rotation = Quaternion.FromToRotation(Vector3.up, DirToPlayer);
        bullet_.GetComponent<Rigidbody2D>().velocity = new Vector2(10f, 10f) * DirToPlayer.normalized;
    }

    bool IsObstacleBetweenEnemyAndPlayer()
    {
        float distanceToPlayer = DirToPlayer.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (Player.transform.position - transform.position).normalized, distanceToPlayer, obstacleLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            CurHp -= 20;
        }
    }
}
