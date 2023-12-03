using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerWinUI : MonoBehaviour
{
    [SerializeField] private Button menu;
    [SerializeField] private Button quit;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menu.gameObject);

        menu.onClick.AddListener(MainMenu);
        quit.onClick.AddListener(() => Application.Quit());
    }

    void MainMenu()
    {
        IngameManager.IsDead = false;
        IngameManager.IsWin = false;
        SceneManager.LoadScene("MainMenu");
    }
}
