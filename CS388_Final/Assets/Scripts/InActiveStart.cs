using UnityEngine;

public class InActiveStart : MonoBehaviour
{
    public GameObject[] objects;

    void Start()
    {
        foreach (var obj in objects)
        {
            obj.SetActive(false);
        }
    }
}
