using UnityEngine;
using UnityEngine.AI;

public class PriestAI : MonoBehaviour
{
    public float attackRange = 2f;
    public float ThrowRange = 6f;
    public int punchDamage = 20;
    public float attackCooldown = 3f;
    public float throwForce = 10.0f;
    public float throwCooldown = 5.0f;
    private float lastThrowTime;
    public Transform stoneSpawnPoint;
    public GameObject stonePrefab;

    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    private BossHealth health;
    private float lastAttackTime;
    private bool isAttacking;
    [SerializeField] private AudioSource Footsteps;
    [SerializeField] private AudioSource Roar;

    private void Start()
    {
        health = GetComponent<BossHealth>();
        player = ZPlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lastAttackTime = 0f;
        isAttacking = false;
    }

    private void Update()
    {
        if (health.IsDead())
        {
            agent.isStopped = true;
            return;
        }

        if (!isAttacking)
        {
            ChasePlayer();
        }


        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= attackRange)
        {
            if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                AttackPunch();
            }
        }

        if (distance <= ThrowRange)
        {
            if (!isAttacking && Time.time - lastThrowTime >= throwCooldown)
            {
                RangedAttack();
            }
            
        }
    }

    private void ChasePlayer()
    {
        Footsteps.Play();
        anim.SetBool("Walking", true);
        anim.SetBool("Idle", false);
        agent.isStopped = false;
        Vector3 playerPosition = player.transform.position;
        agent.SetDestination(playerPosition);
    }

    private void AttackPunch()
    {
        Roar.Play();
        anim.SetBool("Walking", false);
        agent.isStopped = true;
        isAttacking = true;
        // Play punch animation
        anim.SetTrigger("Punch");

        // Optionally, add logic to rotate the priest towards the player

        // Deal damage to the player after the animation is complete
        Invoke("DamagePlayerWithPunch", 1.0f);
    }

    private void DamagePlayerWithPunch()
    {
        // Check if the player is within attack range
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(punchDamage);
                Debug.Log("Punch With Damage");
            }
        }
        else
        {
            Debug.Log("Punch Without Damage");
        }
        isAttacking = false;
        lastAttackTime = Time.time;
        agent.isStopped = true;

    }

    void RangedAttack()
    {
        Roar.Play();
        anim.SetBool("Walking", false);
        agent.isStopped = true;
        isAttacking = true;
        anim.SetTrigger("Throw");
        Invoke("ThrowStone", .8f);
    }

    private void ThrowStone()
    {
        if (Time.time - lastThrowTime >= throwCooldown)
        {
            isAttacking = true;
            agent.isStopped = true;
            // Face the player.
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.forward = directionToPlayer;

            // Spawn and throw a stone.
            GameObject stone = Instantiate(stonePrefab, stoneSpawnPoint.position, Quaternion.identity);
            Rigidbody stoneRigidbody = stone.GetComponent<Rigidbody>();
            stoneRigidbody.AddForce(directionToPlayer * throwForce, ForceMode.Impulse);
        }
        isAttacking = false;
        lastThrowTime = Time.time;
        agent.isStopped = true;

    }

}
