using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Attack Settings")]
    public int attackDamage = 10;
    public float attackRate = 1.5f;
    private float nextAttackTime = 0f;

    [Header("Animation Settings")]
    public float attackDistance = .5f;
    public Animator animator;

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

        // Automatically finds the Animator if one has not been assigned in the Inspector.
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ensures that the player exists and the navigation agent is active.
        if(playerTransform != null && agent != null)
        {
            // Calculates the distance between the enemy and the player.
            float distanceToPlayer = Vector3.Distance(
                transform.position,
                playerTransform.position
            );

            // Checks whether the enemy is close enough to attack.
            if (distanceToPlayer <= attackDistance)
            {
                // Stops the enemy from moving while attacking.
                agent.isStopped = true;

                // Plays the attack animation.
                if (animator != null)
                {
                    animator.SetBool("IsAttacking", true);
                }

                // Deals damage according to the attack cooldown.
                if (Time.time >= nextAttackTime)
                {
                    PlayerHP playerHP = playerTransform.GetComponent<PlayerHP>();

                    if (playerHP != null)
                    {
                        playerHP.TakeDamage(attackDamage);
                        nextAttackTime = Time.time + attackRate;
                    }
                    else
                    {
                        Debug.LogWarning(
                            "EnemyMovement: PlayerHP script not found on the Player GameObject."
                        );
                    }
                }
            }
            else
            {
                // Allows the enemy to move toward the player again.
                agent.isStopped = false;
                agent.SetDestination(playerTransform.position);

                // Stops the attack animation when the player moves away.
                if (animator != null)
                {
                    animator.SetBool("IsAttacking", false);
                }
            }
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
