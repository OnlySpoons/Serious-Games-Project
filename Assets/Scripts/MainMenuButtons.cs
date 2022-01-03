//Default
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added for scripting
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class MainMenuButtons : MonoBehaviour
{
    //Initialises variables
    public AudioMixer m_audioMixer;
    public TMP_Dropdown m_resolutionDropdown;
    public TMP_Dropdown m_qualityDropdown;

    Resolution[] m_resolutionsArray;

    //Runs before program loads
    void Start()
    {
        //Adds all current resolutions on computer to array
        m_resolutionsArray = Screen.resolutions;

        //Clears out previous resolutions from unity dropdown
        m_resolutionDropdown.ClearOptions();

        //Creates string list to pass into AddOptions
        List<string> optionsTemp = new List<string>();

        int currentResolutionIndex = 0;

        //Loops through array adding options to string list
        for(int i = 0; i < m_resolutionsArray.Length; i++)
        {
            string option = m_resolutionsArray[i].width + " x " + m_resolutionsArray[i].height;
            optionsTemp.Add(option);

            //Checks if current i = current screen resolution and makes it the dropdown default
            if(m_resolutionsArray[i].width == Screen.width && 
                m_resolutionsArray[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        //Add filled string list to array
        m_resolutionDropdown.AddOptions(optionsTemp);
        m_resolutionDropdown.value = currentResolutionIndex;
        m_resolutionDropdown.RefreshShownValue();

        //Auto sets quality dropdown
        m_qualityDropdown.value = QualitySettings.GetQualityLevel();
        m_qualityDropdown.RefreshShownValue();
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
        m_audioMixer.SetFloat("VolumeParam", _volume);
    }

    public void SetQuality(int _qualityIndex)
    {
        _qualityIndex = m_qualityDropdown.value;
        QualitySettings.SetQualityLevel(_qualityIndex);
    }

    public void SetFullscreen(bool _isFullscreen)
    {
        Screen.fullScreen = _isFullscreen;
    }

    public void SetResolution(int _resolutionIndex)
    {
        Resolution resolution = m_resolutionsArray[_resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
