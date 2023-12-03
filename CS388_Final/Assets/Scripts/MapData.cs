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
        player.transform.position = new Vector3(playerSpawnPosition.position.x, playerSpawnPosition.position.y, 0);
        player.GetComponent<PlayerData>().RemainEnemy.SetMax(MaxEnemy);
    }
}
