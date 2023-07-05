using UnityEngine;
using UnityEngine.InputSystem;

public class SmolBolts : MonoBehaviour
{
    [Header("Smol Bolts references")]
    [SerializeField] GameObject boltPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float fireForce;
    [SerializeField] float fireRate;
    [SerializeField] Animator playerAnim;
    private float fireRateTimer;
    private bool canShoot;

    private const string isShootingStr = "isShooting";

    private void Awake()
    {
        canShoot = true;
    }
    public void ShootBolts(InputAction.CallbackContext ctx)
    {   
        if(ctx.action.triggered && canShoot)
        {   
            playerAnim.SetBool(isShootingStr, true);
            GameObject bolt = Instantiate(boltPrefab, firePoint.position, firePoint.rotation);
            bolt.GetComponent<Rigidbody>().AddForce(firePoint.forward * fireForce, ForceMode.Impulse);
            fireRateTimer = 0f;
            canShoot=false;
        }
        else
        {
            playerAnim.SetBool(isShootingStr, false);
        }
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if(fireRateTimer > fireRate) {
               canShoot = true;
        }
    }

}
