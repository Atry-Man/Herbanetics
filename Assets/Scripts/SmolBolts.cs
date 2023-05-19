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
    [SerializeField] Animator playerAnim;

    private const string isShootingStr = "isShooting";


    public void ShootBolts(InputAction.CallbackContext ctx)
    {   
        if(ctx.action.triggered)
        {   
            playerAnim.SetBool(isShootingStr, true);
            GameObject bolt = Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
            bolt.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
        }
        else
        {
            playerAnim.SetBool(isShootingStr, false);
        }
    }
       
}
