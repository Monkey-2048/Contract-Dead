using System;
using UnityEngine;

public class OrkHealth : MonoBehaviour
{
    public int maxHealth = 100;  // Maximum health of the Ork
    public int currentHealth;    // Current health of the Ork
    private static bool Dead;   // Event to trigger when the Ork dies
    private Animator anim;
    private OrkAI orkAI;
    public static event EventHandler OnOrkDeath;

    void Start()
    {
        Dead = false;
        anim = GetComponent<Animator>();
        orkAI = GetComponent<OrkAI>();
        currentHealth = maxHealth; // Initialize current health to max health
    }

    // Function to allow other scripts to damage the Ork
    public void TakeDamage(int damageAmount)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damageAmount;

            if (currentHealth > 0)
            {
                if (currentHealth % 200 == 0)
                {
                    orkAI.SpawnCreatures(); // Spawn creatures when health decreases in increments of -200
                }
            }
        }
        if (currentHealth <= 0)
        {
            Dead = true;
            anim.SetTrigger("Death");
            OnOrkDeath?.Invoke(this, EventArgs.Empty);
            Invoke("Die", 4f);
        }
    }

    void Die()
    {
        gameObject.SetActive(false); // Disable or remove the Ork GameObject
        
    }

    public static bool IsDead()
    {
        return Dead;
    }
}
