
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausedText;
    private bool isPaused = false;

    void Start()
    {
        pausedText.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            isPaused = !isPaused;

            pausedText.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
