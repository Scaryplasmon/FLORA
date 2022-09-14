using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using StarterAssets;

public class EnemyAi : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;


    [SerializeField] float health, maxHealth = 3f;
    [SerializeField] private Transform pfDropTurd2;
    [SerializeField] private Transform dieTurdPosition;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    
    private Animator animator;

    private void Awake()
    {
        player = GameObject.Find("PlayerArmature").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();


        health = maxHealth;
        
    }


    // Update is called once per frame
    void Update()
    {
        //Check for Sight and AttackRange
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        
        if (!playerInSightRange && playerInAttackRange) AttackPlayer();

        if (playerInAttackRange) AttackPlayer();
    }

    private void Patroling(){
        
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //walkpointReached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }   

    private void SearchWalkPoint(){

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);  

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer(){

        agent.SetDestination(player.position);
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));

    }

    private void AttackPlayer(){

        //MakeSureEnemyStayStill
        agent.SetDestination(transform.position);
        

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //AttackCodeHere
            animator.SetLayerWeight(1, 1f);
            animator.SetLayerWeight(2, 0f);
            
            

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }

    }

    private void ResetAttack(){

        alreadyAttacked= false;
        animator.SetLayerWeight(1, 0f);
        animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
    }

    public void TakeDamage(int damage){

        health -= damage;
        

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);

    }

    private void DestroyEnemy(){

        Destroy(gameObject);
        
        Vector3 aimDir = dieTurdPosition.position.normalized;
        Instantiate(pfDropTurd2,dieTurdPosition.position, Quaternion.LookRotation(aimDir, Vector3.up));
        
    }

    private void OnDrawGizmosSelected(){

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


}
