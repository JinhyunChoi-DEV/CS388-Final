/*
* Name: PlayerAnimation
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

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] PlayerState playerState;
    [SerializeField] private PlayerAim aim;
    [SerializeField] private Transform renderObject;

    private int idleId;
    private int moveId;
    private int dodgeId;
    private int angle;
    private int deadId;

    void Start()
    {
        idleId = Animator.StringToHash("Idle");
        moveId = Animator.StringToHash("Move");
        dodgeId = Animator.StringToHash("Dodge");
        angle = Animator.StringToHash("Angle");
        deadId = Animator.StringToHash("Dead");
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, aim.Angle < 0 ? 180 : 0, 0));

        animator.SetBool(deadId, !PlayerData.IsAlive);
        animator.SetBool(dodgeId, playerState.State == State.Dodge || playerState.State == State.DoingDodge);
        animator.SetBool(idleId, playerState.State == State.Idle);
        animator.SetBool(moveId, playerState.State == State.Move);

        if (playerState.State != State.DoingDodge)
            animator.SetFloat(angle, Mathf.Abs(aim.Angle));
    }

    public void DeadPlayer()
    {
        IngameManager.IsDead = true;
    }
}
