
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  public float health = 100f;
  public ParticleSystem deathEffect;
  public NavMeshAgent agent;
  public Transform player;
  public LayerMask whatIsGround, whatIsPlayer;

  //Patroling
  public Vector3 walkPoint;
  bool walkPointSet;
  public float walkPointRange;

  //Attacking
  public float timeBetweenAttack;
  bool alreadyAttacked;

  //States
  public float sightRange, attackRange;
  public bool playerInSightRange, playerInAttackRange;

  private void Awake()
  {
    player = GameObject.Find("Player").transform;
    agent = GetComponent<NavMeshAgent>();


  }
  void Update()
  {
    //Check for SightRange and AttackRange
    playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

    if (!playerInSightRange && !playerInAttackRange) Patroling();
    if (playerInSightRange && !playerInAttackRange) ChasePlayer();
    if (playerInSightRange && playerInAttackRange) AttackPlayer();

  }

  private void Patroling()
  {
    if (!walkPointSet) SearchWalkPoint();

    if (walkPointSet)
      agent.SetDestination(walkPoint);

    Vector3 distanceToWalkPoint = transform.position - walkPoint;
    //Walkpoint reached
    if (distanceToWalkPoint.magnitude < 1f)
      walkPointSet = false;
  }
  private void SearchWalkPoint()
  {
    //Calculate random point in range
    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    float randomX = Random.Range(-walkPointRange, walkPointRange);

    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
      walkPointSet = true;
  }
  private void AttackPlayer()
  {
    agent.SetDestination(transform.position);

    transform.LookAt(player);

    if (!alreadyAttacked)
    {

      //Attack code which u want





      alreadyAttacked = true;
      Invoke(nameof(ResetAttack), timeBetweenAttack);
    }


  }
  private void ResetAttack()
  {
    alreadyAttacked = false;
  }
  private void ChasePlayer()
  {
    agent.SetDestination(player.position);
  }
  public void TakeDamage(float amount)
  {
    health -= amount;

    if (health <= 0f)
    {
      Die();
    }
  }

  void Die()
  {

    Destroy(gameObject);

    Instantiate(deathEffect, transform.position, Quaternion.identity);

  }

}
