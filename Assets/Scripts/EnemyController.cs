using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
    Animator enemyAnim;

    private const string isMoving = "isMoving";
    private const string isAttacking = "isAttacking";
    [SerializeField] float attackRange;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
    }

    

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if(distanceToTarget <= attackRange) {

            enemyAnim.SetBool(isMoving, false);
            //enemyAnim.SetBool(isAttacking, true);
        }
        else
        {
            enemyAnim.SetBool(isMoving, true);
            //enemyAnim.SetBool(isAttacking, false);
            agent.SetDestination(target.position);
        }
       
       
    }
}
