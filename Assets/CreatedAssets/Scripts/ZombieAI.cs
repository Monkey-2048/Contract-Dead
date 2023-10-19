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
    private ZombieHealth zombieHealth;
    private Vector3 roamPosition;
    private ZombieVisuals zombieVisuals;
    private float nextRoamTime;
    [SerializeField]private int attackDamage = 10;
    private float lastAttackTime;
    private float attackCooldown = 3.0f;

    public enum ZombieState
    {
        Roaming,
        Chasing,
        Attacking,
        Dead
    }

    public ZombieState currentState = ZombieState.Roaming;

    private void Start()
    {
        target = ZPlayerManager.instance.player.transform;
        agent = GetComponent < NavMeshAgent>();
        anim = Visuals.GetComponent <Animator>();
        zombieVisuals = GetComponent<ZombieVisuals>();
        zombieHealth = GetComponent<ZombieHealth>();
        nextRoamTime = Time.time + Random.Range(0, roamInterval); // Set initial roam time
        GetNewRoamPosition();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (zombieHealth.IsDead())
        {
            currentState = ZombieState.Dead;
            return;
        }

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
        agent.speed = 3f;
        SetAnimation("Walk", true);
        SetAnimation("Attack", false);
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
        agent.speed = 5f;
        SetAnimation("Walk", true);
        SetAnimation("Attack", false);
        zombieVisuals.LookAtPlayer();
        agent.SetDestination(target.position);
    }

    private void Attack()
    {
        SetAnimation("Walk", false);
        SetAnimation("Attack", true);
        zombieVisuals.LookAtPlayer();
        agent.isStopped = true;
        Invoke("DamagePlayer", 2f);
    }

    private void DamagePlayer()
    {
        if (currentState == ZombieState.Attacking)
        {
            PlayerHealth playerHealth = target.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        if (currentState != ZombieState.Dead)
        {
            agent.isStopped = false;
        }
        
    }

    void SetAnimation(string paramName, bool value)
    {
        // Set the specified animation parameter
        anim.SetBool(paramName, value);
    }

}
