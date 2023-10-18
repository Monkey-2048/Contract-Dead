using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject zombiePrefab;     // Reference to the zombie prefab to spawn.
    public Transform player;             // Reference to the player's transform.
    public float spawnRadius = 10.0f;    // Maximum distance from the player to spawn zombies.
    public LayerMask groundLayer;        // Layer mask to check if a position is on the ground.

    void Start()
    {
        // Spawn zombies periodically, you can adjust this as needed.
        InvokeRepeating("SpawnZombie", 5.0f, 10.0f);
    }

    void SpawnZombie()
    {
        // Calculate a random position within the spawnRadius.
        Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
        randomPosition += player.position;

        // Ensure the position is on the ground.
        RaycastHit hit;
        if (Physics.Raycast(randomPosition + Vector3.up * 10.0f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Instantiate a zombie at the valid position.
            GameObject zombie = Instantiate(zombiePrefab, hit.point, Quaternion.identity);

            // Optionally, add logic to make the zombies chase the player or follow a predefined path.
        }
    }
}
