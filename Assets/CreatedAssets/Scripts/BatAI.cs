using UnityEngine;
using UnityEngine.AI;

public class BatAI : MonoBehaviour
{
    public float detectionRange = 10.0f; // Range to detect the player
    public float attackRange = 2.0f; // Range for attacking the player
    public float attackCooldown = 2.0f; // Cooldown between attacks
    public int attackDamage = 10; // Amount of damage to inflict
    private Transform player; // Reference to the player's transform

    private NavMeshAgent agent;
    private bool isChasing;
    private float lastAttackTime;

    void Start()
    {
        player = ZPlayerManager.instance.player;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= detectionRange)
        {
            // Start chasing the player
            isChasing = true;
            ChasePlayer();

            if (distance <= attackRange)
            {
                // Check if it's time to attack
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    lastAttackTime = Time.time;
                    AttackPlayer();
                }
            }
        }
        else if (isChasing)
        {
            // Stop chasing when the player is out of range
            isChasing = false;
        }
    }

    void ChasePlayer()
    {
        if (player != null)
        {
            // Set the player's position as the destination
            agent.SetDestination(player.position);
            LookAtPlayer();
        }
    }

    void AttackPlayer()
    {
        // Deal damage to the player
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void LookAtPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the rotation needed to face the player
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer + new Vector3(0, 0, 90));

            // Apply the rotation directly to the bat's object
            transform.rotation = lookRotation;
        }
    }
}