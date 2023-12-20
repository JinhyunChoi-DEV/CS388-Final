/*
* Name: PlayerUIHolder
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

public class PlayerUIHolder : MonoBehaviour
{
    public GameObject playingPanel;
    public PlayerDeadUI deadUI;
    public PlayerWinUI winUI;

    private bool isActiveEnd = false;

    private void Start()
    {
        playingPanel.SetActive(true);
        deadUI.Panel.SetActive(false);
        winUI.Panel.SetActive(false);
        isActiveEnd = false;
    }

    void Update()
    {
        if (IngameManager.IsDead && !isActiveEnd)
        {
            deadUI.Panel.SetActive(true);
            deadUI.SetButtonSelect();
            isActiveEnd = true;

            Time.timeScale = 0.0f;
        }

        if (IngameManager.IsWin && !isActiveEnd)
        {
            winUI.Panel.SetActive(true);
            winUI.SetButtonSelect();
            isActiveEnd = true;

            Time.timeScale = 0.0f;
        }

        playingPanel.SetActive(!IngameManager.IsDead && !IngameManager.IsWin);
    }
}
