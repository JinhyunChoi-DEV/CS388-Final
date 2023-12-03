using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeadUI : MonoBehaviour
{
    [SerializeField] private Button restart;
    [SerializeField] private Button menu;
    [SerializeField] private Button quit;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(restart.gameObject);

        restart.onClick.AddListener(Restart);
        menu.onClick.AddListener(MainMenu);
        quit.onClick.AddListener(() => Application.Quit());
    }

    void Restart()
    {
        IngameManager.IsDead = false;
        IngameManager.IsWin = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MainMenu()
    {
        IngameManager.IsDead = false;
        IngameManager.IsWin = false;
        SceneManager.LoadScene("MainMenu");
    }
}
