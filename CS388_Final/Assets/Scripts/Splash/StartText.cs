/*
* Name: StartText
* Author: Hyosang Jeong
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    public float blinkInterval = 0.5f; 

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        InvokeRepeating("ToggleVisibility", 0f, blinkInterval);
    }
    private void ToggleVisibility()
    {
        textMesh.enabled = !textMesh.enabled;
    }
}
