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
