using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickup : MonoBehaviour
{
    [SerializeField] GameObject pickupEffect;
    public static event Action<int> AddScore;
    [SerializeField] int scoreIncremenet;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(pickupEffect, transform.position, Quaternion.identity);
            AddScore?.Invoke(scoreIncremenet);
            Destroy(gameObject);
        }
    }
}
