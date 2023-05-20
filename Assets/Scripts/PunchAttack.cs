using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchAttack : MonoBehaviour
{
    int collisionCount;
    [SerializeField] int maxCollisions;
    private void OnTriggerEnter(Collider other)
    {
        collisionCount++;
        Debug.Log(collisionCount);
        if(other.gameObject.CompareTag("Enemy") && collisionCount<= maxCollisions)
        {
            Debug.Log("Hit");
            collisionCount = 0;
        }
    }

    
}
