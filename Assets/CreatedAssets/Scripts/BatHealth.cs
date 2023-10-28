using UnityEngine;

public class BatHealth : MonoBehaviour
{
    public int maxHealth = 50; // Maximum health of the bat
    private int currentHealth; // Current health of the bat
    private bool dead;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    // Function to allow other scripts to damage the bat
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                dead = true;
                Die();
            }
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    void Die()
    {
        // Handle bat death logic here (e.g., play death animation, remove or deactivate the bat, etc.)
        Destroy(gameObject); // For example, destroy the bat game object when it dies
    }
}
