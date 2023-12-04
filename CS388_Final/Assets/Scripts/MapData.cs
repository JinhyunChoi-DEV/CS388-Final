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
