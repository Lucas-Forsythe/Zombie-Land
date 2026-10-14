using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class EnemyHP : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    private int currentHP;

    [UnitHeaderInspectable("Visual Feedback")]
    public float flashDuration = 0.1f;
    private Renderer enemyRenderer;
    private Color ogColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHP = maxHP;
        
        enemyRenderer = GetComponent<Renderer>();

        if (enemyRenderer != null)
        {
            ogColor = enemyRenderer.material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Damage function to be called by other scripts
    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;

        if (enemyRenderer != null)
        {
            StartCoroutine(FlashRed());
        }

        if (currentHP <= 0)
        {
            Die();
        }
    }

    // Coroutine handles pausing game clock to create the flash effect when the enemy takes damage
    private System.Collections.IEnumerator FlashRed()
    {

        enemyRenderer.material.color = Color.red;
        

        yield return new WaitForSeconds(flashDuration);

        enemyRenderer.material.color = ogColor;
    }

    // A temporary placeholder function for when health hits zero
    private void Die()
    {
        Debug.Log(gameObject.name + "was slain!!!");
        Destroy(gameObject);
    }
}
