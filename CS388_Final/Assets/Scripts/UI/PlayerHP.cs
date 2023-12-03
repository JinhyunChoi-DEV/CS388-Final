using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite halfHeart;
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject deadImage;
    [SerializeField] private Image prefab;

    private List<Image> hpObjects = new List<Image>();

    void Start()
    {
        prefab.gameObject.SetActive(false);

        for (int i = 0; i < data.MaxHP; ++i)
        {
            var newHeart = Instantiate(prefab, root.transform);
            newHeart.gameObject.SetActive(true);
            hpObjects.Add(newHeart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var currentHP = data.CurrentHP;

        if (currentHP > 0)
            UpdateCurrentHealth(currentHP);

        root.gameObject.SetActive(currentHP > 0);
        deadImage.gameObject.SetActive(currentHP <= 0);
    }

    void UpdateCurrentHealth(float currentHP)
    {
        var existDecimal = false;
        var roundHP = GetRoundInt(currentHP, ref existDecimal);
        for (int i = 0; i < data.MaxHP; ++i)
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
