using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    [Min(0)]
    public int enemyCount = 5;

    public Transform[] spawnPoints;

    public float spawnInterval = 2f;

    IEnumerator Start()
    {
        if (enemyPrefab == null || spawnPoints == null ||
            spawnPoints.Length == 0)
        {
            Debug.LogError("Assign an enemy prefab and spawn points.");
            yield break;
        }

        Vector3[] positions = new Vector3[spawnPoints.Length];

        // Check every spawn point before spawning.
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (spawnPoints[i] == null ||
                !NavMesh.SamplePosition(
                    spawnPoints[i].position,
                    out NavMeshHit hit,
                    2f,
                    NavMesh.AllAreas))
            {
                Debug.LogError(
                    $"Spawn point {i} is missing or too far from the NavMesh."
                );
                yield break;
            }

            positions[i] = hit.position;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            // Cycles through the spawn points in order.
            int pointIndex = i % spawnPoints.Length;

            Instantiate(
                enemyPrefab,
                positions[pointIndex],
                spawnPoints[pointIndex].rotation
            );

            // Wait before spawning the next enemy.
            if (i < enemyCount - 1)
            {
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }
}