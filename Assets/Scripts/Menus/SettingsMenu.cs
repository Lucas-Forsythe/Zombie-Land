using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private TMP_Dropdown sprintModeDropdown;

    private void Start()
    {
        RefreshSettings();
    }

    public void RefreshSettings()
    {
        // Volume
        volumeSlider.value = Settings.Volume;

        // Sprint mode
        sprintModeDropdown.value =
            (int)Settings.SprintMode;

        Apply();
    }

    public void Apply()
    {
        Settings.Volume = volumeSlider.value;

        audioMixer.SetFloat("Volume", Mathf.Log10(Settings.Volume) * 20f);

        Settings.SprintMode = (PlayerMovement.SprintMode)sprintModeDropdown.value;
    }
}
