using UnityEngine;

public class SmashDamage : MonoBehaviour
{
    [Tooltip("The amount of damage the smash attack deals to players.")]
    public int smashDamage = 25;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the entering object is the player.
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                // Apply damage to the player.
                playerHealth.TakeDamage(smashDamage);
            }
        }
    }
}
