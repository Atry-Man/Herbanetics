using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Combo Variables")]
    private int comboCount;
    private float lastAttackTime;
    [SerializeField] private float comboTimer;
    [SerializeField] string[] comboAttack;
    [SerializeField] Animator playerAnim;
    [SerializeField] float DefaultimeBeforeMovement;
    [SerializeField] float lastAttackDelay;


    [Header("External Variables")]
    [SerializeField] PlayerController playerController;
    [SerializeField] BoxCollider[] ComboColliders; 
   
    private void Awake()
    {
        ResetCombo();
        
    }


    public void OnTripleWhammy(InputAction.CallbackContext context)
    {

        if(context.performed)
        {
            if(Time.time - lastAttackTime > comboTimer) {

                ResetCombo();
               
            }

            if (comboCount < comboAttack.Length)
            {
                playerAnim.SetTrigger(comboAttack[comboCount]);
                playerController.CanMove = false;
                playerController.StopMovement();
              


                if (comboCount <= 1)
                {

                    ComboColliders[comboCount].enabled = true;
                }

                comboCount++;
                lastAttackTime = Time.time;

                if (comboCount == 2)
                {
                    DefaultimeBeforeMovement = lastAttackDelay;
                    ComboColliders[0].enabled = true;
                    ComboColliders[1].enabled = true;
                }
            }
        }
        else
        {
            StartCoroutine(DiableCollidersAndResumeMovement()); 
        }
    }


    void ResetCombo()
    {
        comboCount = 0;
        lastAttackTime = 0;
        DefaultimeBeforeMovement =  1.25f;
       

    }
    
    IEnumerator DiableCollidersAndResumeMovement()
    {
        yield return new WaitForSeconds(DefaultimeBeforeMovement);
        foreach(Collider col  in ComboColliders)
            {
                col.enabled = false;
            }
        playerController.CanMove = true;
    }

    
}
