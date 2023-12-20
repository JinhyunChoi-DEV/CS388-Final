/*
* Name: EnemyAnimation
* Author: Jinhyun Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

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
