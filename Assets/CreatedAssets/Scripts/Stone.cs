using UnityEngine;

public class Stone : MonoBehaviour
{
    public int damage = 20;  // Amount of damage the stone inflicts.

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            Invoke("DestroyStone", 5f);
        }

        Invoke("DestroyStone", 5f);
    }

    private void DestroyStone()
    {
        Destroy(gameObject);
    }
}
