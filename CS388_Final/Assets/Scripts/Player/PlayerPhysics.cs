using UnityEngine;

public class PlayerPhysics : MonoBehaviour
{
    [SerializeField] private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerData.IsAlive)
        {
            collider.enabled = false;
        }
    }
}
