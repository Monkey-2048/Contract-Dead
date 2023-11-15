using UnityEngine;
using UnityEngine.SceneManagement;

public class BossActivationZone : MonoBehaviour
{
    [SerializeField] private GameObject boss;  // Reference to the boss GameObject.
    private PriestHealth bossHealth;
    private PriestAI priestAI;
    [SerializeField] private GameObject bossHealthUI;
    public float activationRadius = 10f;  // Radius within which the boss activates.

    public static bool playerEnteredZone = false;
    private static bool hasActivated = false;  // Keep track if the boss has already been activated in this session.
    [SerializeField] private AudioSource RitualSound;
    [SerializeField] private Transform Ambience;
    [SerializeField] private Transform FightMusic;

    private void Start()
    {
        playerEnteredZone = false;
        bossHealth = boss.GetComponent<PriestHealth>();
        priestAI = boss.GetComponent<PriestAI>();
        bossHealth.enabled = false;
        priestAI.enabled = false;
        bossHealthUI.SetActive(false);
        Ambience.gameObject.SetActive(true);
        FightMusic.gameObject.SetActive(false);

        
    }

    private void Update()
    {
        if (!playerEnteredZone)
        {
            RitualSound.Play();
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

        Ambience.gameObject.SetActive(false);
        FightMusic.gameObject.SetActive(true);

        // Set the hasActivated variable to true.
        hasActivated = true;

        // Disable this activation zone script, as it's no longer needed.
        DisableActivationZone();
    }

    private void DisableActivationZone()
    {
        // Disable this activation zone script.
        enabled = false;

        // If you want to reset the activation state when the scene is reloaded, you can use SceneManager.LoadScene.
        // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    
}
