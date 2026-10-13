using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private Transform playerTransform;
    private NavMeshAgent agent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Fetches the NavMeshAgent component that is attached to the enemy object.
        agent = GetComponent<NavMeshAgent>();

        // searches the entire scene to find the player GameObject.
        GameObject playerObject = GameObject.Find("Player");

        // If the Player exists, Lock onto their transform coordinates. This is a safety check.
        if(playerObject != null)
        {
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("EnemyMovement: Could not find GameObject named 'Player' in the scene!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ensures that the player exists and the navigation agent is active.
        if(playerTransform != null && agent != null)
        {
            // Gives the navigation gps the player's exact real-time 3D position.
            agent.SetDestination(playerTransform.position);
        }
    }
}
