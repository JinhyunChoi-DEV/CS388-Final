using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerWinUI : MonoBehaviour
{
    public GameObject Panel;
    [SerializeField] private Button menu;
    [SerializeField] private Button quit;

    public AudioClip Button_Clip;

    public void SetButtonSelect()
    {
        if (Application.platform != RuntimePlatform.Switch)
            Cursor.visible = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menu.gameObject);
    }

    void Start()
    {
        menu.onClick.AddListener(MainMenu);
        quit.onClick.AddListener(Quit);
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
