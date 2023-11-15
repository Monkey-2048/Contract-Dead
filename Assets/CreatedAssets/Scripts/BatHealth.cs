using UnityEngine;

public class BatHealth : MonoBehaviour
{
    public int maxHealth = 50; // Maximum health of the bat
    private int currentHealth; // Current health of the bat
    private bool dead;

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
        OrkHealth.OnOrkDeath += OrkHealth_OnOrkDeath;
    }

    private void OrkHealth_OnOrkDeath(object sender, System.EventArgs e)
    {
        TakeDamage(maxHealth);
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
                Destroy(gameObject);
            }
        }
    }

    public bool IsDead()
    {
        return dead;
    }

    private void OnDisable()
    {
        OrkHealth.OnOrkDeath -= OrkHealth_OnOrkDeath;
    }

}
