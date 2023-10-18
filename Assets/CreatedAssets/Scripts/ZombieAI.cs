using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    [SerializeField]private float roamRadius = 10f;
    [SerializeField]private float chaseRadius = 20f;
    [SerializeField]private float attackRange = 2f;
    [SerializeField]private float roamInterval = 5f; // Time interval between roam destinations
     
    private Transform target;
    [SerializeField]private Transform Visuals;
    private NavMeshAgent agent;
    private Animator anim;
    private Vector3 roamPosition;
    private ZombieVisuals zombieVisuals;
    private float nextRoamTime;
    [SerializeField]private int attackDamage = 10;
    private float lastAttackTime;
    private float attackCooldown = 3.0f;

    private enum ZombieState
    {
        Roaming,
        Chasing,
        Attacking
    }

    private ZombieState currentState = ZombieState.Roaming;

    private void Start()
    {
        target = ZPlayerManager.instance.player.transform;
        agent = GetComponent < NavMeshAgent>();
        anim = Visuals.GetComponent <Animator>();
        zombieVisuals = GetComponent<ZombieVisuals>();
        nextRoamTime = Time.time + Random.Range(0, roamInterval); // Set initial roam time
        GetNewRoamPosition();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        switch (currentState)
        {
            case ZombieState.Roaming:
                Roam();
                if (distance <= chaseRadius)
                {
                    currentState = ZombieState.Chasing;
                }
                break;

            case ZombieState.Chasing:
                Chase();
                if (distance <= attackRange)
                {
                    currentState = ZombieState.Attacking;
                }
                else if (distance > chaseRadius)
                {
                    currentState = ZombieState.Roaming;
                }
                break;

            case ZombieState.Attacking:
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
                if (distance > attackRange)
                {
                    currentState = ZombieState.Chasing;
                }
                break;
        }
    }

    private void Roam()
    {
        // Check if it's time to change roam destination
        if (Time.time >= nextRoamTime)
        {
            GetNewRoamPosition();
            nextRoamTime = Time.time + roamInterval;
        }

        // Move towards the current roam position
        agent.SetDestination(roamPosition);
    }

    private void GetNewRoamPosition()
    {
        // Generate a random position within the roamRadius
        roamPosition = Random.insideUnitSphere * roamRadius;
        roamPosition += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(roamPosition, out hit, roamRadius, 1);
        roamPosition = hit.position;
    }

    private void Chase()
    {
        zombieVisuals.LookAtPlayer();
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        zombieVisuals.LookAtPlayer();
        PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        
    }
}
