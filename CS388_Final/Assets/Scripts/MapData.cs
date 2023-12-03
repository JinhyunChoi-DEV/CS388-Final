using UnityEngine;

public class MapData : MonoBehaviour
{
    public Transform playerSpawnPosition;

    public void SpawnPlayer(Transform playerTransform)
    {
        playerTransform.position = new Vector3(playerSpawnPosition.position.x, playerSpawnPosition.position.y, 0);
    }
}
