using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour
{
    public Transform bulletStartTransform;
    public Transform targetTransform;
    public float MaxHP;
    public float MinDistanceToPlayer = 8;
    public LayerMask obstacleLayer;
    public NavMeshAgent Agent;
    public Samira samira;
    public Talon talon;
    public Zerry zerry;
    public float FireDelay;

    public bool IsMove { get; private set; }
    public float CurrentHP;
    private Vector2 DirToPlayer;
    private GameObject player;
    private bool waitFire;
    private Coroutine fireCoroutine;

    public AudioSource Boss_Audio;

    public AudioClip Damaged_Clip;
    public AudioClip Death_Clip;
    public AudioClip Samira_Clip;
    public AudioClip Talon_Clip;
    public AudioClip Zerry_Clip;

    public void ApplyDamage(float damage)
    {
        Boss_SFX("Damaged", Damaged_Clip);
        CurrentHP -= damage;

        if (CurrentHP <= 0)
        {
            Boss_SFX("Death", Death_Clip);

            Destroy(gameObject);
            IngameManager.IsWin = true;
        }
    }

    void Start()
    {
        player = GameObject.Find("Player");
        waitFire = false;
        DirToPlayer = (player.gameObject.transform.position - gameObject.transform.position);
    }

    void Update()
    {
        if (CurrentHP <= 0)
        {
            return;
        }

        DirToPlayer = (player.gameObject.transform.position - gameObject.transform.position);

        RotateTransform();

        float distanceToPlayer = DirToPlayer.magnitude;

        if (distanceToPlayer > MinDistanceToPlayer)
        {
            Agent.SetDestination(player.transform.position);
            IsMove = true;
        }
        else
        {
            if (!IsObstacleBetweenEnemyAndPlayer())
            {
                UpdateFire();
                Agent.ResetPath();
                IsMove = false;
            }
            else
            {
                Agent.SetDestination(player.transform.position);
                IsMove = true;
            }
        }
    }

    void UpdateFire()
    {
        if (waitFire)
            return;

        if (fireCoroutine != null)
            StopCoroutine(fireCoroutine);

        fireCoroutine = StartCoroutine(Fire());
    }

    IEnumerator Fire()
    {
        waitFire = true;
        var r = Random.Range(0, 3);

        if (r == 0)
        {
            Boss_SFX("Zerry", Zerry_Clip);

            zerry.Fire(DirToPlayer);
        }
        else if (r == 1)
        {
            Boss_SFX("Talon", Talon_Clip);

            talon.Fire(DirToPlayer);
        }
        else
        {
            Boss_SFX("Samira", Samira_Clip);

            samira.Fire();
        }

        float timer = 0;
        while (timer < FireDelay)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        waitFire = false;
    }

    private void RotateTransform()
    {
        float deg = Mathf.Atan2(DirToPlayer.y, DirToPlayer.x) * Mathf.Rad2Deg;
        var newAngle = Mathf.Abs(deg) <= 90 ? 0 : 180.0f;
        targetTransform.transform.localRotation = Quaternion.Euler(0, newAngle, 0);
    }

    bool IsObstacleBetweenEnemyAndPlayer()
    {
        float distanceToPlayer = DirToPlayer.magnitude;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (player.transform.position - transform.position).normalized, distanceToPlayer, obstacleLayer);

        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }

    private void Boss_SFX(string sfxName, AudioClip clip)
    {
        GameObject sound = new GameObject(sfxName + "Sound");
        Boss_Audio = sound.AddComponent<AudioSource>();
        Boss_Audio.clip = clip;
        Boss_Audio.Play();

        Destroy(sound, clip.length);
    }
}
