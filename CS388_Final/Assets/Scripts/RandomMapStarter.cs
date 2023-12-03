using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomMapStarter : MonoBehaviour
{
    public List<GameObject> mapList = new List<GameObject>();
    public GameObject bossMap;
    private int RandNum = 0;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < mapList.Count; i++)
        {
            mapList[i].SetActive(false);
        }
        RandNum = Random.Range(0, mapList.Count);
        mapList[RandNum].SetActive(true);
        mapList[RandNum].transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
//Sec += Time.deltaTime;
//if (Sec > 1)
//{
//    RandNum = Random.Range(0, projectile.Count);

//    var _projectile = Instantiate(projectile[RandNum], launchPoint.position, launchPoint.rotation);

//    Fire_Sound.Play();

//    _projectile.GetComponent<Rigidbody>().velocity = -launchPoint.right * launchVelocity;
//    Sec = 0;
//}