using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    [SerializeField] public float mouseSensitivity = 200f;

    [Header("Controller Settings")]
    [SerializeField] public float controllerSensitivity = 100f;

    public Transform cam;

    private float xRotation = 0f;
    private Vector2 lookInput;

    private bool usingController = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseAndControllerLook();
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();

        // Check which device is currently providing the input
        if (Gamepad.current != null &&
            Gamepad.current.rightStick.ReadValue().magnitude > 0.1f)
        {
            usingController = true;
        }
        else if (Mouse.current != null &&
                 Mouse.current.delta.ReadValue().magnitude > 0.1f)
        {
            usingController = false;
        }
    }

    void HandleMouseAndControllerLook()
    {
        float sensitivity;

        if (usingController)
        {
            sensitivity = controllerSensitivity;
        }
        else
        {
            sensitivity = mouseSensitivity;
        }

        float mouseX = lookInput.x * sensitivity * Time.deltaTime;
        float mouseY = lookInput.y * sensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);

        // Clear the input after using it
        lookInput = Vector2.zero;
    }
}