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
