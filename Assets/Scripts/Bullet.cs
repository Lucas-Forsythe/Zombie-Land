using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 20;
    public float lifetime = 5f;

    private bool hasHit = false;

    void Start()
    {
        // Removes bullets that miss everything.
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasHit) return;
        hasHit = true;

        EnemyHP enemyHP =
            collision.collider.GetComponentInParent<EnemyHP>();

        if (enemyHP != null)
        {
            enemyHP.TakeDamage(damage);
            Debug.Log("Bullet damaged " + enemyHP.name);
        }

        // Disappears after hitting an enemy, wall, or floor.
        Destroy(gameObject);
    }
}