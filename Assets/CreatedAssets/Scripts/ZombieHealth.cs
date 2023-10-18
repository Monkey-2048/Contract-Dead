using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Implement death behavior here (e.g., play death animation or destroy the GameObject)
        Destroy(gameObject);
    }
}
