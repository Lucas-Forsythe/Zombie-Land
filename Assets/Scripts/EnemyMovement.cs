using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 10;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;

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

    private void OnCollisionStay(Collision collision)
    {
        // Checks if the object the enemy object collided with has the PlayerHP script.

        PlayerHP playerHP = collision.gameObject.GetComponent<PlayerHP>();

        // If the object has the PlayerHP script, check the attack cooldown clock.
        if (playerHP != null && Time.time >= nextAttackTime)
        {
            // Deals the damage.
            playerHP.TakeDamage(attackDamage);

            // Sets the time for when the enemy is allowed to hit again.
            nextAttackTime = Time.time + attackRate;
        }
    }
}
