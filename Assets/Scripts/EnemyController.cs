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
    [SerializeField] int enemyDamage;
    int collisionCount;
    int maxCollisions;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyAnim = GetComponent<Animator>();
        maxCollisions = 1;
        target = GameObject.FindWithTag("Player").transform;
    }

   
    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if(distanceToTarget <= attackRange) {

            enemyAnim.SetBool(isMoving, false);
            enemyAnim.SetBool(isAttacking, true);
        }
        else
        {
            enemyAnim.SetBool(isMoving, true);
            enemyAnim.SetBool(isAttacking, false);
            agent.SetDestination(target.position);
        }
       
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        collisionCount++;

        if(collision.gameObject.CompareTag("Player") && collisionCount<= maxCollisions)
        {
            PlayerDamage playerDamage = collision.gameObject.GetComponent<PlayerDamage>();
            playerDamage.TakeDamage(enemyDamage);
            collisionCount = 0;
        }
    }
}
