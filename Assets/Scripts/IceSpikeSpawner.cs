using UnityEngine;

public class IceSpikeSpawner : MonoBehaviour
{
    public GameObject iceSpikePrefab; // Reference to the ice spike prefab
    public float spawnInterval = 2f; // Time between spawns
    public float spawnRangeX = 8f;   // Horizontal range for random spawning
    private float timer; // Timer to track spawn intervals

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Spawn an ice spike when the timer exceeds the interval
        if (timer >= spawnInterval)
        {
            SpawnIceSpike();

            // Reset the timer
            timer = 0f;
        }
    }

    void SpawnIceSpike()
    {
        // Randomize the X position within the spawn range
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);

        // Calculate the spawn position
        Vector3 spawnPosition = new Vector3(randomX, transform.position.y, 0);

        // Instantiate the ice spike prefab at the spawn position
        Instantiate(iceSpikePrefab, spawnPosition, Quaternion.identity);
    }
}
