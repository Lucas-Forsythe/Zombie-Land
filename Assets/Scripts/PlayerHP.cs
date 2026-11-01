using NUnit.Framework;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
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

    [Header("Death Animation Settings")]
    public float deathDuration = 1.0f;
    public float tiltAngle = 60f;




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
            StartCoroutine(PlayerDie());
        }
    }


    private IEnumerator PlayerDie()
    {
        // Tells the Player they died.
        Debug.Log("Player has died! Restarting ther level!");


        UnityEngine.InputSystem.PlayerInput input = GetComponent<UnityEngine.InputSystem.PlayerInput>();
        if (input != null)
        {
            ((MonoBehaviour)input).enabled = false;
        }

        Vector3 startPosition = cameraTransform.localPosition;
        Quaternion startRotation = cameraTransform.localRotation;

        Vector3 targetPosition = new Vector3(startPosition.x, 0.2f, startPosition.z);
        Quaternion targetRotation = Quaternion.Euler(startRotation.eulerAngles.x, startRotation.eulerAngles.y, tiltAngle);

        float elapsedTime = 0f;
        // Changes the camera's values when the player dies.
        while (elapsedTime < deathDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / deathDuration;

            cameraTransform.localPosition = Vector3.Lerp(startPosition, targetPosition, t);
            cameraTransform.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);

            yield return null;
        }

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
