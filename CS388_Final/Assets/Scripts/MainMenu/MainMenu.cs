/*
* Name: MainMenu
* Author: Jaewoo Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

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
