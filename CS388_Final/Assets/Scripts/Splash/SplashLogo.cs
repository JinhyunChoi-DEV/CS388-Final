/*
* Name: SplashLogo
* Author: Hyosang Jeong
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using nn.hid;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashLogo : MonoBehaviour
{
    private NpadId npadId = NpadId.Handheld;
    private NpadState npadState = new NpadState();

    void Start()
    {
        if (Application.platform == RuntimePlatform.Switch)
        {
            Npad.Initialize();
            Npad.SetSupportedIdType(new NpadId[] { NpadId.Handheld, NpadId.No1 });
            Npad.SetSupportedStyleSet(NpadStyle.FullKey | NpadStyle.Handheld | NpadStyle.JoyDual);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Switch)
        {
            var style = Npad.GetStyleSet(NpadId.Handheld);
            Npad.GetState(ref npadState, npadId, style);

            if ((npadState.GetButton(NpadButton.L) && npadState.GetButton(NpadButton.R)))
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
        else
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
