using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthFillImage; // Reference to the UI Image component representing the health bar fill.
    public PriestHealth bossHealth; // Reference to the BossHealth script.

    private void Start()
    {
        // Ensure that the healthFillImage and bossHealth references are set in the Inspector.
        if (healthFillImage == null || bossHealth == null)
        {
            Debug.LogError("HealthBar is missing references to healthFillImage or bossHealth.");
            enabled = false; // Disable the script if references are missing.
        }
        PriestHealth.OnBossDeath += BossHealth_OnBossDeath;
    }

    private void BossHealth_OnBossDeath(object sender, System.EventArgs e)
    {
        enabled = false;
    }

    private void Update()
    {
        UpdateHealthBarFill();
    }

    private void UpdateHealthBarFill()
    {
        // Calculate the fill amount as a ratio of current health to max health.
        float fillAmount = (float)bossHealth.currentHealth / bossHealth.maxHealth;

        // Set the health bar's fill amount.
        healthFillImage.fillAmount = fillAmount;
        
    }

    private void OnDestroy()
    {
        PriestHealth.OnBossDeath -= BossHealth_OnBossDeath;
    }
}
