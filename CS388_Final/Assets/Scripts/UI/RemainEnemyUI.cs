using TMPro;
using UnityEngine;

public class RemainEnemyUI : MonoBehaviour
{
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
