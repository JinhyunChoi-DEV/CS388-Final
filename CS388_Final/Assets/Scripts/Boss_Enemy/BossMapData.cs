/*
* Name: BossMapData
* Author: Jinhyun Chou
* Copyright © 2023 DigiPen (USA) LLC. and its owners. All Rights
Reserved.
No parts of this publication may be copied or distributed,
transmitted, transcribed, stored in a retrieval system, or
translated into any human or computer language without the
express written permission of DigiPen (USA) LLC., 9931 Willows
Road NE, Redmond, WA 98052, USA.
 */

using UnityEngine;

public class BossMapData : MonoBehaviour
{
    [SerializeField] private Transform bossSpawnTransform;
    [SerializeField] private Transform playerStartPosition;

    public void StartBossRoom(GameObject player)
    {
        var boss = Resources.Load<Boss>("Boss");
        var position = new Vector3(bossSpawnTransform.position.x, bossSpawnTransform.position.y, 0);
        var newBoss = Instantiate(boss, position, Quaternion.identity);
        newBoss.CurrentHP = boss.MaxHP;

        var com = player.GetComponent<PlayerData>();
        player.transform.position = new Vector3(playerStartPosition.position.x, playerStartPosition.position.y, 0);
        com.BossUI.SetData(newBoss);
        com.RemainEnemy.Panel.gameObject.SetActive(false);
    }
}
