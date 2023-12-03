using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator animator;

    private int idleId;
    private int walkId;
    private int deadId;

    void Start()
    {
        idleId = Animator.StringToHash("Enemy_Idle");
        walkId = Animator.StringToHash("Enemy_Walk");
        deadId = Animator.StringToHash("Enemy_Dead");
    }

    void Update()
    {
        animator.SetBool(idleId, !enemy.IsMove);
        animator.SetBool(walkId, enemy.IsMove);
        animator.SetBool(deadId, enemy.IsDead);
    }

    public void DeadEnemy()
    {
        enemy.DestroyObject();
    }
}
