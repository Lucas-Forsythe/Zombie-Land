using NUnit.Framework;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHP : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    private int currentHP;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // PlayerHP health pool initializing on startup.
        currentHP = maxHP;
    }

    //Enemies will call this function when they hit the player.
    public void TakeDamage(int damageAmount)
    {
        currentHP -= damageAmount;
        Debug.Log("Player took damage! Current HP: " + currentHP);

        if (currentHP <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        // Tells the Player they died.
        Debug.Log("Player has died! Restarting ther level!");

        // Fetches the scene's build index number and tells Unity to reload the scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
