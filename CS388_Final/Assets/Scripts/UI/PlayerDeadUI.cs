using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDeadUI : MonoBehaviour
{
    public GameObject Panel;
    [SerializeField] private Button restart;
    [SerializeField] private Button menu;
    [SerializeField] private Button quit;

    public AudioClip Button_Clip;

    public void SetButtonSelect()
    {
        if (Application.platform != RuntimePlatform.Switch)
            Cursor.visible = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(restart.gameObject);
    }

    void Start()
    {
        restart.onClick.AddListener(Restart);
        menu.onClick.AddListener(MainMenu);
        quit.onClick.AddListener(Quit);
    }

    void Restart()
    {
        SoundManager.instance.SFXPlay("Button", Button_Clip);
        IngameManager.IsDead = false;
        IngameManager.IsWin = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void MainMenu()
    {
        SoundManager.instance.SFXPlay("Button", Button_Clip);
        IngameManager.IsDead = false;
        IngameManager.IsWin = false;
        SceneManager.LoadScene("MainMenu");
    }

    void Quit()
    {
        SoundManager.instance.SFXPlay("Button", Button_Clip);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
