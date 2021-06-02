using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform endPoint;
    public Transform startPoint;

    // Start is called before the first frame update
    void Start()
    {
        WorldController.instanse.OnPlatformMovement += TryDelAndAddPlatform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TryDelAndAddPlatform()
    {
        if(transform.position.z < WorldController.instanse.minZ)
        {
            WorldController.instanse.worldBuilder.CreatePlatform();
            Destroy(gameObject);
        }
        
    }

    private void OnDestroy()
    {
        WorldController.instanse.OnPlatformMovement -= TryDelAndAddPlatform;
    }
}
