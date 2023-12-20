/*
* Name: GunTransformModifers
* Author: Jinhyun Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using UnityEngine;

public class GunTransformModifer : MonoBehaviour
{
    [SerializeField] private PlayerAim aim;
    [SerializeField] private Transform targetTransform;
    [SerializeField] private GunTransformData[] datas;
    [SerializeField] private PlayerWeapon playerWeapon;

    void Start()
    {

    }

    void Update()
    {
        var data = GetTargetGunGunTransformData();

        if (data == null)
            return;

        var playerAngle = aim.Angle;
        var weaponAngle = aim.WeaponAngle;

        var currentRotation = targetTransform.localRotation.eulerAngles;

        if (playerAngle < 0)
            currentRotation.y = 180.0f;
        else
            currentRotation.y = 0.0f;

        UpdateActiveTransform(playerAngle, data);
        var zRotation = GetWeaponZRotation(weaponAngle);
        currentRotation.z = zRotation;

        var absPlayerAngle = Mathf.Abs(playerAngle);
        var currentPosition = data.GunMoveTransform.position;
        if (absPlayerAngle >= 0 && absPlayerAngle <= 30)
            data.GunMoveTransform.position = new Vector3(currentPosition.x, currentPosition.y, 2);
        else
            data.GunMoveTransform.position = new Vector3(currentPosition.x, currentPosition.y, -2);

        targetTransform.position = data.GunMoveTransform.position;
        targetTransform.localRotation = Quaternion.Euler(currentRotation);
    }

    private GunTransformData GetTargetGunGunTransformData()
    {
        foreach (var data in datas)
        {
            if (data.Type == playerWeapon.CurrentGun)
                return data;
        }

        return null;
    }

    private void UpdateActiveTransform(float playerAngle, GunTransformData data)
    {
        var rad = Mathf.Deg2Rad * playerAngle;
        var x = data.offset * Mathf.Sin(rad);
        var y = data.offset * Mathf.Cos(rad);
        data.GunMoveTransform.transform.position = data.Center.position + new Vector3(x, y);
    }

    private float GetWeaponZRotation(float weaponAngle)
    {
        if (weaponAngle <= 90 && weaponAngle >= -90.0f)
            return weaponAngle;

        return -180.0f - weaponAngle;
    }
}
