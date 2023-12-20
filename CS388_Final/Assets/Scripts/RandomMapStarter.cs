/*
* Name: RandomMapStater
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

public class RandomMapStarter : MonoBehaviour
{
    public List<MapData> mapList = new List<MapData>();
    public BossMapData bossMap;
    private int RandNum = 0;
    private bool startedBossRoom = false;

    private GameObject player;

    void Awake()
    {
        Time.timeScale = 1.0f;
        for (int i = 0; i < mapList.Count; i++)
        {
            mapList[i].gameObject.SetActive(false);
        }
        RandNum = Random.Range(0, mapList.Count);
        mapList[RandNum].gameObject.SetActive(true);

        bossMap.gameObject.SetActive(false);
        startedBossRoom = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        mapList[RandNum].SpawnPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
        if (MapData.RemainEnemy == 0 && !startedBossRoom)
        {
            startedBossRoom = true;
            bossMap.StartBossRoom(player);
            bossMap.gameObject.SetActive(true);
            mapList[RandNum].gameObject.SetActive(false);
        }
    }
}