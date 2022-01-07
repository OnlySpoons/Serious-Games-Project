using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private Canvas pauseMenu, optionsMenu;

    public void OnContinue()
    {
        pauseMenu.enabled = false;
    }

    public void OnOptions()
    {
        optionsMenu.enabled = true;
    }
    public void OnBack()
    {
        optionsMenu.enabled = false;
    }

    public void OnExit()
    {
        AudioManager.m_instance.StopPlaying("GameMusic");
        SceneManager.LoadScene(0);
        AudioManager.m_instance.Play("MenuMusic");
    }
}
