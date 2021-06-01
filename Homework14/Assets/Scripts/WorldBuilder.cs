using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public GameObject[] freePlatforms;
    public GameObject[] obstaclePlatforms;
    public Transform platformContainer;

    private Transform lastPlatform = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.Q))
       {
            CreateFreePlatform();
       }
    }

    private void CreateFreePlatform()
    {
        
        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position :
            lastPlatform.GetComponent<PlatformController>().endPoint.position;
        int index = Random.Range(0, freePlatforms.Length);
        GameObject res = Instantiate(freePlatforms[index], pos +  new Vector3(0,0,40), Quaternion.identity, platformContainer);
        lastPlatform = res.transform;
    }
}
