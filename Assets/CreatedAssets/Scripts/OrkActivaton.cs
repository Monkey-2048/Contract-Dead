using UnityEngine;

public class OrkActivation : MonoBehaviour
{
    public OrkAI orkAI;
    public OrkHealth orkHealth;
    public Transform bossHealthUI;

    private void Start()
    {
        orkAI.enabled = false;
        orkHealth.enabled = false;
        bossHealthUI.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activate the Ork AI
            orkAI.enabled = true;

            // Activate the Ork Health script
            orkHealth.enabled = true;

            // Activate the Boss Health UI
            bossHealthUI.gameObject.SetActive(true);

            // You can also add any additional logic here, such as displaying a message or performing other actions.
        }
    }
}
