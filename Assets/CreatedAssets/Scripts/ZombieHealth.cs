using UnityEngine;
using UnityEngine.AI;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;
    private bool Dead = false;
    [SerializeField] private Transform visuals;
    

    private void Start()
    {
        animator = visuals.GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Dead = true;
            animator.SetTrigger("Dead");
            ZombieAI zombieAI = GetComponentInParent<ZombieAI>();
            zombieAI.enabled = false;
            Invoke("Die", 5f);
        }
    }

    private void Die()
    {
        // Implement death behavior here (e.g., play death animation or destroy the GameObject)
        
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return Dead;
    }
}
