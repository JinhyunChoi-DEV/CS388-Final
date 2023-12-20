/*
* Name: PlayerDeadHP
* Author: Jinhyun Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject deadImage;
    [SerializeField] private Image prefab;

    private int MaxHP;
    private List<Image> hpObjects = new List<Image>();

    public void SetMaxHP(int maxHP)
    {
        MaxHP = maxHP;
    }

    public void UpdateHP(float currentHP)
    {
        UpdateCurrentHealth(currentHP);
        root.gameObject.SetActive(currentHP > 0);
        deadImage.gameObject.SetActive(currentHP <= 0);
    }

    void Start()
    {
        prefab.gameObject.SetActive(false);

        for (int i = 0; i < MaxHP; ++i)
        {
            var newHeart = Instantiate(prefab, root.transform);
            newHeart.gameObject.SetActive(true);
            hpObjects.Add(newHeart);
        }

        UpdateHP(MaxHP);
    }

    void UpdateCurrentHealth(float currentHP)
    {
        var existDecimal = false;
        var roundHP = GetRoundInt(currentHP, ref existDecimal);
        for (int i = 0; i < MaxHP; ++i)
        {
            var targetIndex = i + 1;
            hpObjects[i].gameObject.SetActive(targetIndex <= roundHP);

            if (targetIndex == roundHP && existDecimal)
                hpObjects[i].sprite = halfHeart;
            else
                hpObjects[i].sprite = fullHeart;
        }
    }

    int GetRoundInt(float hp, ref bool existDecimal)
    {
        var intValue = (int)hp;

        bool needToRound = hp - intValue > 0.0f;
        existDecimal = needToRound;
        if (needToRound)
            return intValue + 1;
        else
            return intValue;
    }
}
