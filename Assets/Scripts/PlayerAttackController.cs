using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Combo Variables")]
    [SerializeField] Animator playerAnim;
    private const string punchStr = "";
 
    public void OnBigPunch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerAnim.SetTrigger(punchStr);
        }
    }  
    
}
