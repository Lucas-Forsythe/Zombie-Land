using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Min(0)]
    public int enemyCount = 5;

    public Transform[] spawnPoints;

    void Start()
    {
        if (enemyPrefab == null || spawnPoints == null ||
            spawnPoints.Length < enemyCount)
        {
            Debug.LogError(
                "Assign an enemy prefab and at least one spawn point per enemy."
            );
            return;
        }

        Vector3[] positions = new Vector3[enemyCount];

        // Check every position before spawning any enemies.
        for (int i = 0; i < enemyCount; i++)
        {
            if (spawnPoints[i] == null ||
                !NavMesh.SamplePosition(
                    spawnPoints[i].position,
                    out NavMeshHit hit,
                    2f,
                    NavMesh.AllAreas))
            {
                Debug.LogError($"Spawn point {i} is missing or too far from the NavMesh.");
                return;
            }

            positions[i] = hit.position;
        }

        // Spawn exactly the requested number.
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(
                enemyPrefab,
                positions[i],
                spawnPoints[i].rotation
            );
        }
    }
}