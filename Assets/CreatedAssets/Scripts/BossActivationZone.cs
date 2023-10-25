using UnityEngine;

public class BossActivationZone : MonoBehaviour
{
    [SerializeField] private GameObject boss;  // Reference to the boss GameObject.
    private BossHealth bossHealth;
    private PriestAI priestAI;
    [SerializeField] private GameObject bossHealthUI;
    public float activationRadius = 10f;  // Radius within which the boss activates.

    public static bool playerEnteredZone = false;

    private void Start()
    {
        bossHealth = boss.GetComponent<BossHealth>();
        priestAI = boss.GetComponent<PriestAI>();
        bossHealth.enabled = false;
        priestAI.enabled = false;
        bossHealthUI.SetActive(false);
    }

    private void Update()
    {
        if (!playerEnteredZone)
        {
            // Check if the player is within the activation radius.
            Collider[] colliders = Physics.OverlapSphere(transform.position, activationRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Player"))
                {
                    ActivateBoss();
                    playerEnteredZone = true;
                    break;
                }
            }
        }
    }

    private void ActivateBoss()
    {
        // Enable the boss GameObject and its AI scripts.
        if (boss != null)
        {
            bossHealth.enabled = true;
            priestAI.enabled = true;
            bossHealthUI.SetActive(true);
        }

        // Disable this activation zone script, as it's no longer needed.
        enabled = false;
    }
}
