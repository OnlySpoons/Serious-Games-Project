using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    //Initialises variables
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

    [SerializeField]
    private TextMeshProUGUI playerNameText;
    [SerializeField]
    private Button playerNameSubmitButton;
    [SerializeField]
    private TMP_InputField playerNameInputField;
    [SerializeField]
    private GameObject playerNamePanel;

    Resolution[] resolutionsArray;

    void Awake()
    {
        playerNameInputField.onValueChanged.AddListener(ValidateNameInput);
    }

    void Start()
    {
        if (GameSettingsScript.GameCompleted)
            playerNamePanel.SetActive(false);

        fullscreenToggle.isOn = Screen.fullScreen;
        resolutionsArray = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> optionsTemp = new List<string>();

        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutionsArray.Length; i++)
        {
            string option = resolutionsArray[i].width + " x " + resolutionsArray[i].height;
            optionsTemp.Add(option);

            if(resolutionsArray[i].width == Screen.width && 
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

        if (PlayerPrefs.HasKey("Volume"))
            GameSettingsScript.Volume = PlayerPrefs.GetFloat("Volume");

        volumeSlider.value = GameSettingsScript.Volume;
        SetVolume(GameSettingsScript.Volume);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        AudioManager.m_instance.StopPlaying("MenuMusic");
    }

    public void QuitGame()
    {
        Application.Quit();

        //Debug
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
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
        GameSettingsScript.IsFullscreen = fullscreenToggle.isOn;
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = resolutionsArray[_resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetPlayerName()
    {
        GameSettingsScript.PlayerName = playerNameText.text;
    }

    private void ValidateNameInput(string input)
    {
        playerNameSubmitButton.interactable = !string.IsNullOrWhiteSpace(input);
    }
}
