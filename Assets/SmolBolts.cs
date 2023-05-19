using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmolBolts : MonoBehaviour
{
    [Header("Smol Bolts references")]
    [SerializeField] GameObject boltPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireForce;



    public void ShootBolts(InputAction.CallbackContext ctx)
    {   
        if(ctx.action.triggered)
        {
            GameObject bolt = Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
            bolt.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }
    }
       
}
