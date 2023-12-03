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
        }

        if (IngameManager.IsWin && !isActiveEnd)
        {
            winUI.Panel.SetActive(true);
            winUI.SetButtonSelect();
            isActiveEnd = true;
        }

        playingPanel.SetActive(!IngameManager.IsDead && !IngameManager.IsWin);
    }
}
