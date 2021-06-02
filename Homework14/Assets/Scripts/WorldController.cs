using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    public static float levelSpeed = 25f;
    public WorldBuilder worldBuilder;
    public float minZ = 0;

    public delegate void TryToDelAndAddPlatform();
    public event TryToDelAndAddPlatform OnPlatformMovement;

    public static WorldController instanse;

    private void Awake()
    {
        if (WorldController.instanse != null)
        {
            Destroy(gameObject);
            return;
        }
        WorldController.instanse = this;        
    }

    private void OnDestroy()
    {
        WorldController.instanse = null;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(OnPlanformMovementCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * levelSpeed * Time.deltaTime; 
    }
    IEnumerator OnPlanformMovementCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (OnPlatformMovement != null)
                OnPlatformMovement(); 

        }
    }
}
