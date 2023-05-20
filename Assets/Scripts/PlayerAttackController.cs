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
    [SerializeField] Animator handsAnim;
    [SerializeField] float DefaultimeBeforeMovement;
    [SerializeField] float lastAttackDelay;
    [SerializeField] GameObject attackEffect;
    [SerializeField] GameObject attackEffect2;
    [SerializeField] GameObject hands;

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
                hands.SetActive(true);
                playerAnim.SetTrigger(comboAttack[comboCount]);
                handsAnim.SetTrigger(comboAttack[comboCount]);
                attackEffect.SetActive(true);
                attackEffect2.SetActive(true);
                playerController.CanMove = false;
                playerController.StopMovement();
              


                if (comboCount <= 1)
                {

                    ComboColliders[comboCount].enabled = true;
                }

                comboCount++;
                lastAttackTime = Time.time;

                if (comboCount == 3)
                {
                   
                    DefaultimeBeforeMovement = lastAttackDelay;
                    ComboColliders[0].enabled = true;
                    ComboColliders[1].enabled = true;
                    attackEffect.SetActive(true);
                    attackEffect2.SetActive(true);
                    StartCoroutine(DisableHands());
                }
            }
        }
        else
        {
            StartCoroutine(DisableCollidersAndResumeMovement()); 
        }
    }


    void ResetCombo()
    {
        comboCount = 0;
        lastAttackTime = 0;
        DefaultimeBeforeMovement =  1.25f;
       

    }

    IEnumerator DisableHands()
    {
        yield return new WaitForSeconds(1.5f);
        hands.SetActive(false);
    }
    
    IEnumerator DisableCollidersAndResumeMovement()
    {
        yield return new WaitForSeconds(DefaultimeBeforeMovement);
       
        attackEffect.SetActive(false);
        attackEffect2.SetActive(false);
        foreach (Collider col  in ComboColliders)
            {
                col.enabled = false;
            }
        playerController.CanMove = true;
    }

    
}
