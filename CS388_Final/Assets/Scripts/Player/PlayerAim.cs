/*
* Name: PlayerAim
* Author: Jinhyun Choi
* Copyright � 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class PlayerAim : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float Angle { get; private set; }
    public float WeaponAngle { get; private set; }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = false;

        Angle = 0.0f;
    }

    void Update()
    {
        if (!PlayerData.IsAlive)
            return;

        if (PlayerInput.Instance.IsController)
            UpdateByController();
        else
            UpdateByMouse();
    }

    private void UpdateByMouse()
    {
        var camera = Camera.main;
        var mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0.0f;

        var aimDir = (mousePosition - target.position).normalized;
        Angle = Mathf.Atan2(aimDir.x, aimDir.y) * Mathf.Rad2Deg;
        WeaponAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;

        gameObject.transform.position = mousePosition;
    }

    private void UpdateByController()
    {
        var aim = PlayerInput.Instance.InputData.Aim;
        if (aim == Vector2.zero)
            return;

        Angle = Mathf.Atan2(aim.x, aim.y) * Mathf.Rad2Deg;
        var rad = Mathf.Deg2Rad * Angle;
        var x = 3.0f * Mathf.Sin(rad);
        var y = 3.0f * Mathf.Cos(rad);
        gameObject.transform.position = target.position + new Vector3(x, y);
        WeaponAngle = Mathf.Atan2(aim.y, aim.x) * Mathf.Rad2Deg;
    }
}
