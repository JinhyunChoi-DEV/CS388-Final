using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomMapStarter : MonoBehaviour
{
    public List<MapData> mapList = new List<MapData>();
    public MapData bossMap;
    private int RandNum = 0;

    void Awake()
    {
        for (int i = 0; i < mapList.Count; i++)
        {
            mapList[i].gameObject.SetActive(false);
        }
        RandNum = Random.Range(0, mapList.Count);
        mapList[RandNum].gameObject.SetActive(true);

        bossMap.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        var player = GameObject.Find("Player");
        mapList[RandNum].SpawnPlayer(player);
    }

    // Update is called once per frame
    void Update()
    {
    }
}