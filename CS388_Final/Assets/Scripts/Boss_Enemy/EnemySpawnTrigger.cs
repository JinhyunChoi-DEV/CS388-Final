using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] EnemySpawner spawner;
    [SerializeField] private BoxCollider2D cd;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            spawner.Spawn();
            cd.enabled = false;
        }
    }
}
