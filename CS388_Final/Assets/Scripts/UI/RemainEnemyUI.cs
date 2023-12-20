/*
* Name: RemainEnemyUI
* Author: Jinhyun Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using TMPro;
using UnityEngine;

public class RemainEnemyUI : MonoBehaviour
{
    public GameObject Panel;
    [SerializeField] private TMP_Text text;

    private int max;

    public void SetMax(int max)
    {
        this.max = max;
    }

    void Start()
    {
        UpdateText();
    }

    void Update()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        text.text = string.Format("{0} / {1}", MapData.RemainEnemy, max);
    }
}
