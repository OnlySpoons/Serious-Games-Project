using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class BackTMP : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BackToMenu()
    {
        //Iterates to next scene in the build index (File > Build Settings)
        SceneManager.LoadScene(0);

        AudioManager.m_instance.Play("MenuMusic");
    }
}
