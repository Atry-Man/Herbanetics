using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
     PlayerDamage playerDamage;
    [SerializeField] float healthPercentage;

    private void Start()
    {
        playerDamage = GameObject.FindObjectOfType<PlayerDamage>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerDamage.IncreaseHealth(healthPercentage);
            Destroy(gameObject);
        }
    }
}
