/*
* Name: EnemySpawner
* Author: Jinhyun Choi
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> SpawnList = new List<GameObject>();

    public void Spawn()
    {
        var enemy = Resources.Load<Enemy>("Enemy");

        foreach (var spawnPoint in SpawnList)
            Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
    }
}
