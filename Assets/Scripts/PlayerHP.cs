using NUnit.Framework;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHP : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHP = 100;
    private int currentHP;

    [Header("Camera Shake Settings")]
    public Transform cameraTransform;
    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.15f;

    private Vector3 originalCameraPos;
    private bool isShaking = false;






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

        // Activates camera shake.
        if (cameraTransform != null && !isShaking)
        {
            StartCoroutine(ProcessCamerashake());
        }

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

    private IEnumerator ProcessCamerashake()
    {
        isShaking = true;
        originalCameraPos = cameraTransform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float randomX = Random.Range(-1f, 1f) * shakeMagnitude;
            float randomY = Random.Range(-1f, 1f) * shakeMagnitude;

            cameraTransform.localPosition = new Vector3(originalCameraPos.x + randomX, originalCameraPos.y + randomY, originalCameraPos.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = originalCameraPos;
        isShaking = false;
    }
}
