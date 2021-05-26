using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelMove : MonoBehaviour
{
    public static float levelSpeed = 100f;
    public Rigidbody levelRB;
    public Transform levelTransform; 
    // Start is called before the first frame update
    void Start()
    {
        levelRB = GetComponent<Rigidbody>();
        levelTransform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {        
        levelRB.MovePosition(levelTransform.position + Vector3.back * levelSpeed * Time.deltaTime);
    }
}
