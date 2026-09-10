using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount = 2f;
    [SerializeField] private float shakeSpeed = 20f;

    private PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (playerMovement != null && playerMovement.isRunning)
        {
            float sideToSide = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

            // Keep the rotation from PlayerLook and add the shake
            transform.localRotation *= Quaternion.Euler(0f, 0f, sideToSide);
        }
    }
}