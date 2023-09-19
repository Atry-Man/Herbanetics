using UnityEngine;
using UnityEngine.InputSystem;

public class SmolBolts : MonoBehaviour
{
    [Header("Smol Bolts references")]
    [SerializeField] Transform firePoint;
    [SerializeField] Animator playerAnim;
    [SerializeField] SmolBoltsSO SmolBoltsSO;
    private float fireRateTimer;
    private bool canShoot;
    private const string isShootingStr = "isShooting";
    [SerializeField] Transform playerPos;
   

    private void Awake()
    {
        canShoot = true;
    }
    public void ShootBolts(InputAction.CallbackContext ctx)
    {
        if (ctx.action.triggered && canShoot)
        {
            playerAnim.SetBool(isShootingStr, true);

            Vector3 shootingDirection;

            if (ctx.control.device is Mouse)
            {

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                shootingDirection = (mousePos - playerPos.position).normalized;
                Debug.Log(shootingDirection);
            }
            else
            {
               
                shootingDirection = transform.forward;
            }

            GameObject bolt = Instantiate(SmolBoltsSO.boltPrefab, firePoint.position, Quaternion.identity);

            
            bolt.transform.forward = shootingDirection;

            bolt.GetComponent<Rigidbody>().AddForce(shootingDirection * SmolBoltsSO.fireForce, ForceMode.Impulse);
            fireRateTimer = 0f;
            canShoot = false;
        }
        else
        {
            playerAnim.SetBool(isShootingStr, false);
        }
    }

    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if(fireRateTimer > SmolBoltsSO.fireRate) {
               canShoot = true;
        }
    }

}
