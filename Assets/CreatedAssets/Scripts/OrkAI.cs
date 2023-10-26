using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class OrkAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float attackRange = 2.0f;
    [SerializeField] private float attackCooldown = 3.0f;
    [SerializeField] private int attackDamage = 20;
    private Animator anim;
    private NavMeshAgent agent;
    private float animationSpeed = 2f;

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
        lastAttackTime = Time.time;
        anim.speed = animationSpeed;
        currentState = OrkState.Idle;
    }

    private void Update()
    {
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
                    Invoke("DamagePlayer", 1f);
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
        
        EndAttack();
    }

    private void ChasePlayer()
    {
        if (player != null && isChasing)
        {
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
}
