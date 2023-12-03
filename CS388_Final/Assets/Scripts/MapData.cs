using UnityEngine;

public class MapData : MonoBehaviour
{
    [SerializeField] private Transform playerSpawnPosition;

    public void SpawnPlayer(Transform playerTransform)
    {
        playerTransform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, 0);
    }
}
