/*
* Name: BossUI
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
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    [SerializeField] private GameObject bossPanel;
    [SerializeField] private Slider bossHPSlider;

    private bool isSpawnBoss = false;
    private Boss boss;

    public void SetData(Boss boss)
    {
        this.boss = boss;
        isSpawnBoss = true;
        bossPanel.SetActive(true);

        bossHPSlider.value = boss.CurrentHP / boss.MaxHP;
    }

    private void Start()
    {
        isSpawnBoss = false;

        bossPanel.SetActive(false);
    }

    private void Update()
    {
        if (isSpawnBoss)
        {
            bossHPSlider.value = boss.CurrentHP / boss.MaxHP;
        }
    }
}
