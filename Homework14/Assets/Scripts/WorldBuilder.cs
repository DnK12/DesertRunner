using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public GameObject[] rightPlatforms;
    public GameObject[] leftPlatforms;
    public GameObject[] freePlatforms;
    public GameObject[] obstaclePlatforms;
    public Transform platformContainer;
    
    
    

    private int Difficult = 3;

   
    private Transform lastPlatform = null;
    private Transform lastLeftPlatform = null;
    private Transform lastRightPlatform = null;
    // Start is called before the first frame update
    void Start()
    {
        CreateFreePlatform();
        CreateFreePlatform();
        for (int i = 0; i < 5; i++)
        {
            CreatePlatform();
        }
    }

    // Update is called once per frame
    void Update()
    {
       


    }

    public void CreatePlatform()
    {      
        if (Random.Range(1, Difficult) == 0) CreateFreePlatform();
        else CreateObstaclePlatform();       
    }

    private void CreateFreePlatform()
    {
        
        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position :
            lastPlatform.GetComponent<PlatformController>().endPoint.position;
        int index = Random.Range(0, freePlatforms.Length);
        GameObject res = Instantiate(freePlatforms[index], pos +  new Vector3(0,0,40), Quaternion.identity, platformContainer);
        lastPlatform = res.transform;
        CreateDoodadsRight();
        CreateDoodadsLeft();
       
    }
    private void CreateObstaclePlatform()
    {

        Vector3 pos = (lastPlatform == null) ?
            platformContainer.position :
            lastPlatform.GetComponent<PlatformController>().endPoint.position;
        int index = Random.Range(0, obstaclePlatforms.Length);
        GameObject res = Instantiate(obstaclePlatforms[index], pos + new Vector3(0, 0, 40), Quaternion.identity, platformContainer);
        lastPlatform = res.transform;
        CreateDoodadsRight();
        CreateDoodadsLeft();
       
    }
    private void CreateDoodadsRight()
    {

        Vector3 pos = (lastRightPlatform == null) ?
            platformContainer.position :
            lastRightPlatform.GetComponent<PlatformController>().endPoint.position;
        int index = Random.Range(0, rightPlatforms.Length);
        GameObject res = Instantiate(rightPlatforms[index], pos + new Vector3(23.4f, 0, 40), Quaternion.identity, platformContainer);
        lastRightPlatform = res.transform;        
    }
    private void CreateDoodadsLeft()
    {

        Vector3 pos = (lastLeftPlatform == null) ?
            platformContainer.position :
            lastLeftPlatform.GetComponent<PlatformController>().endPoint.position;
        int index = Random.Range(0, leftPlatforms.Length);
        GameObject res = Instantiate(leftPlatforms[index], pos + new Vector3(-200.6f, 0, 40), Quaternion.identity, platformContainer);
        lastLeftPlatform = res.transform;
    }
}
