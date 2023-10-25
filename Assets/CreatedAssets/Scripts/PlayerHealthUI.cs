using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Image healthBarImage;  // Reference to the health bar image component.
    public PlayerHealth playerHealth;  // Reference to the PlayerHealth script.

    private void Start()
    {
        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth reference is not set.");
            return;
        }

        if (healthBarImage == null)
        {
            Debug.LogError("HealthBarImage reference is not set.");
            return;
        }

        // Ensure that the health bar image is correctly initialized.
        UpdateHealthBar(playerHealth.maxHealth);

        // Subscribe to the PlayerDamaged event to update the health bar when the player takes damage.
        playerHealth.OnPlayerDamaged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int currentHealth)
    {
        // Calculate the fill amount for the health bar based on the current health.
        float fillAmount = (float)currentHealth / playerHealth.maxHealth;
        healthBarImage.fillAmount = fillAmount;
    }

    private void OnDestroy()
    {
        // Unsubscribe from the PlayerDamaged event to avoid memory leaks.
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDamaged -= UpdateHealthBar;
        }
    }
}
