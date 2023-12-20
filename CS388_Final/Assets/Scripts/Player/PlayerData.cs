/*
* Name: PlayerData
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

public class PlayerData : MonoBehaviour
{
    public RemainEnemyUI RemainEnemy;
    public BossUI BossUI;

    [SerializeField] private PlayerState state;
    [SerializeField] private PlayerHP hpUI;
    public static bool IsAlive { get; private set; }
    public int MaxHP;
    public float DodgeSpeed;
    public float WalkSpeed;

    public bool IgnoreDamageCollision { get; private set; }
    private float currentHP;

    public AudioClip Damaged_Clip;
    public AudioClip Death_Clip;

    void Awake()
    {
        hpUI.SetMaxHP(MaxHP);
    }

    void Start()
    {
        if (Application.platform != RuntimePlatform.Switch)
            Cursor.visible = false;

        currentHP = MaxHP;
        IsAlive = true;

        IgnoreDamageCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            ApplyDamage();


        if (IsAlive==true && currentHP <= 0)
        {
            SoundManager.instance.SFXPlay("Death", Death_Clip);
            IsAlive = false;
        }

        IgnoreDamageCollision = state.State == State.Dodge || state.State == State.DoingDodge;
    }

    public void ApplyDamage()
    {
        if (currentHP <= 0)
            return;

        if (IgnoreDamageCollision)
            return;

        SoundManager.instance.SFXPlay("Damaged", Damaged_Clip);
        currentHP -= 0.5f;
        hpUI.UpdateHP(currentHP);
    }
}
