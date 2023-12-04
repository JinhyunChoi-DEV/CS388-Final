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
