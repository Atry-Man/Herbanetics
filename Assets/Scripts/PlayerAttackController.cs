using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Combo Variables")]
    [SerializeField] Animator playerAnim;
    private const string punchStr = "Punch";

    [SerializeField] BigPunchSO bigPunchSO;
    [SerializeField] Transform punchPos;
    public static Action StartPunchActiveCooldown;
    private bool canPunch;
    private float fireRateTimer;

    private void Awake()
    {
        canPunch = true;
    }
    public void OnBigPunch(InputAction.CallbackContext context)
    {
        if (context.performed && canPunch)
        {
            playerAnim.SetTrigger(punchStr);
            GameObject punch = Instantiate(bigPunchSO.punchPrefab, punchPos.position, Quaternion.identity);
            punch.GetComponent<Rigidbody>().AddForce(punchPos.forward * bigPunchSO.fireForce, ForceMode.Impulse);
            Vector3 movDir = punchPos.forward.normalized;
            punch.transform.rotation = Quaternion.LookRotation(movDir);
            fireRateTimer = 0f;
            canPunch = false;
            //StartPunchActiveCooldown?.Invoke();
        }
    }  
   
    private void Update()
    {
        fireRateTimer += Time.deltaTime;

        if (fireRateTimer > bigPunchSO.fireRate)
        {
            canPunch = true;
        }
    }
}
