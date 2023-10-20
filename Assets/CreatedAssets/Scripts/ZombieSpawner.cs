using UnityEngine;

public class ZombieSpawnManager : MonoBehaviour
{
    public GameObject[] zombiePrefabs;  // Array of zombie prefabs to choose from.
    public Transform player;            // Reference to the player's transform.
    public float spawnRadius = 50.0f;   // Maximum distance from the player to spawn zombies.
    public LayerMask groundLayer;       // Layer mask to check if a position is on the ground.

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
            // Select a random zombie prefab from the array.
            GameObject randomZombiePrefab = zombiePrefabs[Random.Range(0, zombiePrefabs.Length)];

            // Instantiate the chosen zombie prefab at the valid position.
            GameObject zombie = Instantiate(randomZombiePrefab, hit.point, Quaternion.identity);
        }
    }
}
