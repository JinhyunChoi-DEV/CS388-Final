/*
* Name: MapData
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

public class MapData : MonoBehaviour
{
    public Transform playerSpawnPosition;
    public int MaxEnemy;
    public static int RemainEnemy;

    private void Start()
    {
        RemainEnemy = MaxEnemy;
    }

    public void SpawnPlayer(GameObject player)
    {
        var com = player.GetComponent<PlayerData>();
        player.transform.position = new Vector3(playerSpawnPosition.position.x, playerSpawnPosition.position.y, 0);
        com.RemainEnemy.SetMax(MaxEnemy);
        com.RemainEnemy.Panel.gameObject.SetActive(true);
    }
}
