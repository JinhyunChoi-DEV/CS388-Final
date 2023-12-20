/*
* Name: PlayerPhysics
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

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerData.IsAlive)
        {
            collider.enabled = false;
        }
    }
}
