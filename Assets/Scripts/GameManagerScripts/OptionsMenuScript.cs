using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenuScript : MonoBehaviour
{
    [SerializeField]
    private Canvas optionsMenu;

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private TMP_Dropdown resolutionDropdown;
    [SerializeField]
    private TMP_Dropdown qualityDropdown;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle fullscreenToggle;

    Resolution[] resolutionsArray;

    void Start()
    {
        resolutionsArray = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> optionsTemp = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutionsArray.Length; i++)
        {
            string option = resolutionsArray[i].width + " x " + resolutionsArray[i].height;
            optionsTemp.Add(option);

            if (resolutionsArray[i].width == Screen.width &&
                resolutionsArray[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(optionsTemp);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        qualityDropdown.value = QualitySettings.GetQualityLevel();
        qualityDropdown.RefreshShownValue();
    }

    void OnEnable()
    {
        fullscreenToggle.isOn = GameSettingsScript.IsFullscreen;
        volumeSlider.value = GameSettingsScript.Volume;
        SetVolume(GameSettingsScript.Volume);
    }

    void OnDisable()
    {
        GameSettingsScript.Volume = volumeSlider.value;
        GameSettingsScript.IsFullscreen = fullscreenToggle.isOn;
    }

    public void OnOptions()
    {
        optionsMenu.enabled = true;
    }
    public void OnBack()
    {
        optionsMenu.enabled = false;
    }

    public void SetVolume(float _volume)
    {
        audioMixer.SetFloat("VolumeParam", _volume);
        GameSettingsScript.Volume = volumeSlider.value;

        PlayerPrefs.SetFloat("Volume", _volume);
    }

    public void SetQuality(int _qualityIndex)
    {
        _qualityIndex = qualityDropdown.value;
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void SetFullscreen(bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = resolutionsArray[_resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
