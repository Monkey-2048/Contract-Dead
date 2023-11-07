using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class OrkAI : MonoBehaviour
{
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float attackCooldown = 1.5f;
    [SerializeField] private int attackDamage = 20;
    private Animator anim;
    private NavMeshAgent agent;
    private OrkHealth orkHealth;
    private float animationSpeed = 2f;
    public float rotationSpeed = 5.0f;
    public GameObject creaturePrefab;
    public Transform spawnPoint;
    public int numberOfCreatures = 2;

    private Transform player;
    private float lastAttackTime;
    private int attackCount = 17;
    private bool isChasing;
    private enum OrkState
    {
        Idle,
        Chasing,
        Attacking
    }
    private OrkState currentState;

    private void Start()
    {
        player = ZPlayerManager.instance.player.transform;
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        orkHealth = GetComponent<OrkHealth>();
        lastAttackTime = Time.time;
        anim.speed = animationSpeed;
        currentState = OrkState.Idle;
    }

    private void Update()
    {

        if (orkHealth.IsDead())
        {
            agent.isStopped = true;
            enabled = false;
            return;
        }

        float distance = Vector3.Distance(player.position, transform.position);

        switch (currentState)
        {
            case OrkState.Idle:
                if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
                {
                    int randomAttack = Random.Range(1, attackCount + 1);
                    anim.SetTrigger("Attack" + randomAttack);
                    lastAttackTime = Time.time;
                    currentState = OrkState.Attacking;
                }
                else
                {
                    isChasing = true;
                    currentState = OrkState.Chasing;
                    anim.SetTrigger("Run");
                }
                break;

            case OrkState.Chasing:
                if (distance <= attackRange)
                {
                    isChasing = false;
                    StopChasing();
                    currentState = OrkState.Attacking;
                }
                else if (distance > attackRange)
                {
                    isChasing = true;
                    ChasePlayer();
                }
                break;

            case OrkState.Attacking:
                if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
                {
                    int randomAttack = Random.Range(1, attackCount + 1);
                    anim.SetTrigger("Attack" + randomAttack);
                    lastAttackTime = Time.time;
                    Invoke("DamagePlayer", .1f);
                }
                else if (distance > attackRange)
                {
                    isChasing = true;
                    currentState = OrkState.Chasing;
                }
                break;
        }
    }

    private void DamagePlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        Invoke("EndAttack", .5f);
    }

    private void ChasePlayer()
    {
        if (player != null && isChasing)
        {
            LookAtPlayer();
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    private void StopChasing()
    {
        agent.isStopped = true;
    }

    public void EndAttack()
    {
        isChasing = true;
        currentState = OrkState.Chasing;
        anim.SetTrigger("Run");
    }

    private void LookAtPlayer()
    {
        if (player != null)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Calculate the rotation needed to face the player
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            // Smoothly interpolate between the current rotation and the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SpawnCreatures()
    {
        // Check if the spawn point and creature prefab are set.
        if (creaturePrefab != null)
        {
            for (int i = 0; i < numberOfCreatures; i++)
            {
                // Calculate a random position near the spawn point.
                Vector3 randomOffset = new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f));
                Vector3 spawnPosition = spawnPoint.position + randomOffset;

                // Instantiate a creature at the calculated position.
                GameObject newCreature = Instantiate(creaturePrefab, spawnPosition, spawnPoint.rotation);
            }
        }
    }
}
