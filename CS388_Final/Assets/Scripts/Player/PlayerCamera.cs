using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;

    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (!PlayerData.IsAlive)
            return;

        var playerPosition = playerTransform.position;
        cam.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
    }
}
