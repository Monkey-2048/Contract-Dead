using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the player
    private int currentHealth;    // Current health of the player

    // Optional: Add events for taking damage and dying
    public delegate void PlayerDamaged(int currentHealth);
    public delegate void PlayerDied();
    public PlayerDamaged OnPlayerDamaged;
    public PlayerDied OnPlayerDied;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    // Function to allow other scripts to damage the player
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;
            Debug.Log("Heath Left : " + currentHealth);

            // Optional: Invoke events to notify listeners about damage
            OnPlayerDamaged?.Invoke(currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        // Handle player death logic here (e.g., play death animation, respawn, or game over)
        // Optional: Invoke an event to notify listeners about the player's death
        OnPlayerDied?.Invoke();
        gameObject.SetActive(false);
    }

    // Function to restore the player's health
    public void RestoreHealth(int healAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);

            // Optional: Invoke events to notify listeners about health restoration
            OnPlayerDamaged?.Invoke(currentHealth);
        }
    }
}
