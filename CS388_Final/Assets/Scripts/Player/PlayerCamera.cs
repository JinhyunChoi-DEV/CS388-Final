/*
* Name: PlayerCamera
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

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!PlayerData.IsAlive)
            return;

        var playerPosition = playerTransform.position;
        cam.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
    }
}
