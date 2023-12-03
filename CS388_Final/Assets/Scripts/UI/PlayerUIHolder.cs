using UnityEngine;

public class PlayerUIHolder : MonoBehaviour
{
    public GameObject playingPanel;
    public GameObject deadPanel;
    public GameObject winPanel;

    private void Start()
    {
        playingPanel.SetActive(true);
        deadPanel.SetActive(false);
    }

    void Update()
    {
        playingPanel.SetActive(!IngameManager.IsDead && !IngameManager.IsWin);
        deadPanel.SetActive(IngameManager.IsDead);
        winPanel.SetActive(IngameManager.IsWin);
    }
}
