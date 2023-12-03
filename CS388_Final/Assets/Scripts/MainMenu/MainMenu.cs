using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Paparazzi
{
    public class MainMenu : MonoBehaviour
    {
        public Button startButton;

        void Start()
        {
            EventSystem.current.SetSelectedGameObject(startButton.gameObject);
        }

        // Update is called once per frame
        void Update()
        {
        }

        public void OnClickStart()
        {
            SceneManager.LoadScene("PlayableScene");
        }

        public void OnClickEnd()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
