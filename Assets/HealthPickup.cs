using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    PlayerDamage playerDamage;
    [SerializeField] float healthPercentage;
    [SerializeField] GameObject pickupEffect;

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
