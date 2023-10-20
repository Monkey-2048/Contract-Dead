using UnityEngine;
using UnityEngine.AI;

public class CursedPriestAI : MonoBehaviour
{
    public float attackRange = 2f;
    public int punchDamage = 20;
    public int smashDamage = 30;
    public float attackCooldown = 3f;

    public GameObject smashTriggerZone;
    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    private float lastAttackTime;
    private bool isAttacking;

    private void Start()
    {
        player = ZPlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        lastAttackTime = 0f;
        isAttacking = false;
    }

    private void Update()
    {
        if (!isAttacking)
        {
            ChasePlayer();
        }


        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= attackRange)
        {
            if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                // Perform a random attack
                int randomAttack = Random.Range(0, 2); // 0 for punch, 1 for smash
                if (randomAttack == 0)
                {
                    // Punch attack
                    AttackPunch();
                }
                else
                {
                    // Smash attack
                    AttackPunch();
                }
            }
        }
    }

    private void ChasePlayer()
    {
        anim.SetBool("Walking", true);
        anim.SetBool("Idle", false);
        agent.isStopped = false;
        Vector3 playerPosition = player.transform.position;
        agent.SetDestination(playerPosition);
    }

    private void AttackPunch()
    {
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

    private void AttackSmash()
    {
        agent.isStopped = true;
        isAttacking = true;
        anim.SetTrigger("JumpAttack");

        // Enable the trigger zone to start dealing area damage during the smash animation
        smashTriggerZone.SetActive(true);

        // Wait for the animation to complete (you can adjust the delay based on your animation)
        Invoke("EndSmashAttack", 1.0f);
    }

    private void EndSmashAttack()
    {
        // Disable the trigger zone after the animation is completed
        smashTriggerZone.SetActive(false);

        isAttacking = false;
        lastAttackTime = Time.time;
        agent.isStopped = false;
    }
}
