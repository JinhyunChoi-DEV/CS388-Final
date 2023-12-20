/*
* Name: PlayerMove
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

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private PlayerState state;
    [SerializeField] private PlayerAim aim;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerData data;
    private Vector2 dir;

    public AudioClip Dodge_Clip;

    void Start()
    { }

    void Update()
    {
        if (!PlayerData.IsAlive)
        {
            rb.velocity = new Vector3(0, 0, 0);
            return;
        }

        if (state.State == State.Dodge || state.State == State.DoingDodge)
            Dodge();
        else if (state.State == State.Move)
            Walk();
        else
            Idle();
    }

    private void Dodge()
    {
        if (state.State == State.Dodge)
        {
            SoundManager.instance.SFXPlay("Dodge", Dodge_Clip);
            var aimAngle = aim.Angle;
            dir = new Vector2(Mathf.Sin(aimAngle * Mathf.Deg2Rad), Mathf.Cos(aimAngle * Mathf.Deg2Rad)).normalized;
        }

        rb.velocity = dir * data.DodgeSpeed;
    }

    private void Walk()
    {
        var moveDir = PlayerInput.Instance.InputData.MoveDir;

        rb.velocity = moveDir * data.WalkSpeed;
    }

    private void Idle()
    {
        rb.velocity = Vector3.zero;
    }
}
