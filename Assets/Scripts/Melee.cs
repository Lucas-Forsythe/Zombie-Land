using UnityEngine;
using UnityEngine.Rendering;

public class Melee : MonoBehaviour // <-- MonoBehavior is necessary for the script to fuinction while attached to an object.
{
    // Before being fully functional:
    // Need to add script to a child.
    // Need to assign an enemy to the enemy layer.
    // Need Health script for the enemy.
    // Optional: Maybe we could have a script that allows weapon switch functionality? Or I can change the melee input to another button so that weapon switching won't be necessary.

    // Let's this script know the player's input component exists.
    private UnityEngine.InputSystem.PlayerInput playerInput;

    [Header("Attack Positioning")]

    // Where the attack lands
    public Transform attackPoint;

    // Knife Radius
    public float attackRange = 0.5f;

    // tells code to only attack enemies
    public LayerMask enemyLayers;

    // Bool used to indicate if the player is holding the knife.
    public bool isknifeEquipped = true;

    // Float variable used to determine how long a player must wait between each input.
    public float attackCooldown = 0.5f;

    // A variable to keep track of the exact timestamp of when they can swing next.
    private float nextAttackTime = 0f;

    void Start()
    {

        // Automatically searchezs the GameObject this is on for the PlayerInput component.
        playerInput = GetComponent<UnityEngine.InputSystem.PlayerInput>();

    }

    private void Update()
    {

        if (playerInput == null) return;

        // Dictates whether the knife is being held and if the left mouse button is being clicked.
        if (isknifeEquipped && playerInput.actions["Melee"].triggered)
        {

            // Dictates whether enough time has passed between each swing (input).
            if (Time.time >= nextAttackTime)
            {

                // Triggers the attack
                TriggerAttack();

                // Put's the input on cooldown till the next time to attack.
                nextAttackTime = Time.time + attackCooldown;

            }

        }

    }

    
    void TriggerAttack()
    {
        // Casts aa invisible sphere that detects enemies on the enemy layerf when contact is made.
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider enemy in hitEnemies)
        {
            // Tells us the enemy that was hit
            Debug.Log("You stabbed" + enemy.name);

        }

        // Tells the user the knife was swung.
        Debug.Log("Knife swung!");
                 
    }

    // For debugging purposes.
    void OnDrawGizmosSelected()
    {

        // If an attack point hasn't been assigned yet, don't try to draw anything.
        //This prevents NullReferenceException, a game-crashing error.
        if (attackPoint == null)
        {
            return;
        }

        // Drawing color is red.
        Gizmos.color = Color.red;

        // Draw a wireframe sphere at our attack point using our range value.
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
}
