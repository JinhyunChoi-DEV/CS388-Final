using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] Animator animator;

    private int idleId;
    private int walkId;

    void Start()
    {
        idleId = Animator.StringToHash("Boss_Idle");
        walkId = Animator.StringToHash("Boss_Walk");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(idleId, !boss.IsMove);
        animator.SetBool(walkId, boss.IsMove);
    }
}
