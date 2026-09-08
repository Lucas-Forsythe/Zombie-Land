using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeAmount = 2f;
    [SerializeField] private float shakeSpeed = 20f;

    private Quaternion initialRotation;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        initialRotation = transform.localRotation;

        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (playerMovement != null && playerMovement.isRunning)
        {
            float sideToSide = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

            transform.localRotation = initialRotation * Quaternion.Euler(0f, 0f, sideToSide);
        }
        else
        {
            transform.localRotation = initialRotation;
        }
    }
}