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
    [SerializeField] GameObject hands;


    [Header("External Variables")]
    [SerializeField] PlayerController playerController;
    [SerializeField] BoxCollider[] ComboColliders;


    private Coroutine disableHandsCoroutine;


    private void Awake()
    {
        ResetCombo();
        
    }


    public void OnTripleWhammy(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (Time.time - lastAttackTime > comboTimer)
            {
                ResetCombo();
            }

            if (comboCount < comboAttack.Length)
            {
                hands.SetActive(true);
                playerAnim.SetTrigger(comboAttack[comboCount]);
                handsAnim.SetTrigger(comboAttack[comboCount]);

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
                    if (disableHandsCoroutine != null)
                    {
                        StopCoroutine(disableHandsCoroutine);
                    }
                    disableHandsCoroutine = StartCoroutine(DisableHands(1.5f));
                }
                else
                {
                   
                    if (disableHandsCoroutine != null)
                    {
                        StopCoroutine(disableHandsCoroutine);
                    }

                    
                }
            }
        }
        else
        {
            StartCoroutine(DisableCollidersAndResumeMovement());
            if (comboCount != 3)
            {
                if (disableHandsCoroutine != null)
                {
                    StopCoroutine(disableHandsCoroutine);
                }
                disableHandsCoroutine = StartCoroutine(DisableHands(0.75f));
            }
        }
    }


    void ResetCombo()
    {
        comboCount = 0;
        lastAttackTime = 0;
        DefaultimeBeforeMovement =  1.25f;
       

    }

    IEnumerator DisableHands(float delay)
    {
        yield return new WaitForSeconds(delay);
        hands.SetActive(false);
    }

    IEnumerator DisableCollidersAndResumeMovement()
    {
        yield return new WaitForSeconds(DefaultimeBeforeMovement);
        foreach (Collider col  in ComboColliders)
            {
                col.enabled = false;
            }
        playerController.CanMove = true;
    }

    
}
