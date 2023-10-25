using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;

public class BossHealth : MonoBehaviour
{
    [SerializeField] public int maxHealth = 500; // Maximum health of the boss.
    public int currentHealth;  // Current health of the boss.
    private Animator anim;
    private bool Dead;
    public static event EventHandler OnBossDeath;

    // You can add more variables for UI elements, such as health bars or effects.

    private void Start()
    {
        anim = GetComponent<Animator>();
        currentHealth = maxHealth; // Initialize the current health to the maximum health.
    }

    // Function to deal damage to the boss.
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Decrease the boss's health by the damage amount.

        // You can add more logic here, such as updating UI elements to reflect the boss's health.

        if (currentHealth <= 0)
        {
            PriestAI priestAI = GetComponent<PriestAI>();
            priestAI.enabled = false;
            anim.SetTrigger("Dead");
            OnBossDeath?.Invoke(this, EventArgs.Empty);
            Invoke("Die", 5f);
        }
    }

    // Function to handle the boss's death.
    private void Die()
    {
        Destroy(gameObject);
    }

    public bool IsDead()
    {
        return Dead;
    }

}
