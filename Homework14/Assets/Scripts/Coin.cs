using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ParticleSystem PickUpCoin;
    int Score = 0;
    public int CoinCost = 1;
    public float rotSpeed = 1f;
    public void TakeACoin()
    {
        Score=+CoinCost;
        GameObject.Instantiate(PickUpCoin, transform.position, transform.rotation);        
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.rotation = transform.rotation * Quaternion.Euler(0, 0 , rotSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TakeACoin();            
        }
    }
}
