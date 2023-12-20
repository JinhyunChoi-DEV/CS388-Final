/*
* Name: BossAnimation
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
