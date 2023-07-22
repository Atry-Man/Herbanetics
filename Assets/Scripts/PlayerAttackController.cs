using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Combo Variables")]
    [SerializeField] Animator playerAnim;
    private const string punchStr = "Punch";

    [SerializeField] Transform punchPos;
    [SerializeField] GameObject punchObj;
    [SerializeField] float punchForce;
    public void OnBigPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerAnim.SetTrigger(punchStr);
            ActivatePunch();
        }
    }  
    

    public void ActivatePunch()
    {
        GameObject punch = Instantiate(punchObj, punchPos.position, Quaternion.identity);
        punch.GetComponent<Rigidbody>().AddForce(punchPos.forward * punchForce, ForceMode.Impulse);
    }
}
