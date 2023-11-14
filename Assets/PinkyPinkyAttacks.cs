using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PinkyPinkyAttacks : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] Transform playerPos;
    [SerializeField] float movSpeed;
    [SerializeField] float minimumDistance;
    Animator animator;
    private const string isMoving = "Walk";
    private const string isAttacking = "Attack";
    private const string isStunned = "isStunned";
    private bool canAttack;
    private bool canMove;
    [SerializeField] PinkyPinkyController pinkyPinkyController;
    [SerializeField] int numOfAttack;
    private int attackCounter;
    [SerializeField] int secondPhaseNumberOfAttacks;
    [SerializeField] float stunDuration;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        canAttack = false;
        canMove = true;
    }

    public void RollToPlayer()
    {
        if(Vector3.Distance(transform.position, playerPos.position) > minimumDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPos.position, movSpeed * Time.deltaTime);
            LookAtTarget(playerPos.position);
        }
        else
        {
            AttackTransition();
            canMove = false;
            canAttack = true;
        }
    }

    private void LookAtTarget(Vector3 position)
    {
        Vector3 lookPos = (position - transform.position).normalized;
        lookPos.y = 0f;
        quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    void AttackTransition()
    {
        animator.SetBool(isMoving, false);
        animator.SetBool(isAttacking, true);
    }

    public void AttackCounter()
    {
        if (canAttack)
        {
            if(!pinkyPinkyController.isInSecondPhase && attackCounter < numOfAttack)
            {
                attackCounter++;
                
            }else if(!pinkyPinkyController.isInSecondPhase && attackCounter >= numOfAttack)
            {
                StunTransition();
                attackCounter = 0;

            }else if(pinkyPinkyController.isInSecondPhase && attackCounter < numOfAttack)
            {
                attackCounter++;

            }else if(pinkyPinkyController.isInSecondPhase && attackCounter >= numOfAttack)
            {
                StunTransition();
                attackCounter = 0;
            }
        }
    }
    void RollTransition()
    {
        canMove = true;
        animator.SetBool(isMoving, true);
        animator.SetBool(isAttacking, false);
    }

    void StunTransition()
    {
        canMove = false;
        canAttack = false;
        animator.SetBool(isMoving, false);
        animator.SetBool(isAttacking,false);
        animator.SetBool(isStunned, true);
        StartCoroutine(StunCooldown());
    }

    IEnumerator StunCooldown()
    {
        yield return new WaitForSeconds(stunDuration);
        animator.SetBool(isStunned, false);
        RollTransition();
    }

    private void Update()
    {
        if (canMove)
        {
            RollToPlayer();
        }

        if (canAttack)
        {
            LookAtTarget(playerPos.position);
        }

        if (pinkyPinkyController.isBossDefeated)
        {
            canAttack = false;
        }

        if (pinkyPinkyController.isInSecondPhase)
        {
            numOfAttack = secondPhaseNumberOfAttacks;
        }
    }
}
