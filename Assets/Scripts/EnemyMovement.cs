using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    NavMeshAgent agent;
    Transform target;
   // Animator enemyAnim;

    private const string isRunning = "isRunning";
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        //enemyAnim = GetComponent<Animator>();
      
    }

    private void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        //enemyAnim.SetBool(isRunning, true);
        agent.SetDestination(target.position);
    }
}
