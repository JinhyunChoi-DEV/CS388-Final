using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Button startButton;
    public AudioClip Button_Clip;

    void Start()
    {
        if (Application.platform != RuntimePlatform.Switch)
            Cursor.visible = true;

        EventSystem.current.SetSelectedGameObject(startButton.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnClickStart()
    {
        SoundManager.instance.SFXPlay("Button", Button_Clip);
        SceneManager.LoadScene("PlayableScene");
    }

    public void OnClickEnd()
    {
        SoundManager.instance.SFXPlay("Button", Button_Clip);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
