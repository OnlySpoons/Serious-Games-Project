using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField]
    private Canvas pauseMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pauseMenu.enabled = !pauseMenu.enabled;
    }

    public void OnContinue()
    {
        pauseMenu.enabled = false;
    }

    public void OnExit()
    {
        AudioManager.m_instance.StopPlaying("GameMusic");
        SceneManager.LoadScene(0);
        AudioManager.m_instance.Play("MenuMusic");
    }
}
