using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HeavyWeapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    public int attackDamage = 25;
    public float attackRange = 5f;
    public float attackCooldown = 1.5f;
    public float shockwaveForce = 20f;

    [Header("References")]
    public Transform attackPoint;         
    public LayerMask enemyLayer;          

    [Header("Inventory Status")]
    public bool isUnlocked = false;

    private float nextAttackTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUnlocked) return;

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            TriggerAttack();
        }
    }

    private void TriggerAttack()
    {
        if (Time.time < nextAttackTime) return;
        nextAttackTime = Time.time + attackCooldown;

        ExecuteShockwave();
    }

    private void ExecuteShockwave()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            EnemyHP enemyHealth = enemy.GetComponent<EnemyHP>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log("The Sledge Hammer hit the enemy!");
            }

            NavMeshAgent agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                Vector3 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                knockbackDirection.y = 0;
                agent.velocity = knockbackDirection * shockwaveForce;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
