using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage videoScreen;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;

    void OnEnable()
    {
        ObjectiveManagerScript.onVictoryAchieved += StartVideo;
    }

    void OnDisable()
    {
        ObjectiveManagerScript.onVictoryAchieved -= StartVideo; 
    }

    void Update()
    {
        audioSource.volume = GameSettingsScript.Volume;
        Debug.Log($"AS vol {audioSource.volume.ToString()}");
        Debug.Log($"GS vol {GameSettingsScript.Volume}");
    }

    void StartVideo()
    {
        audioSource.volume = GameSettingsScript.Volume;

        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        videoPlayer.Prepare();

        int i = 0;

        while(!videoPlayer.isPrepared)
        {
            yield return i;
        }

        videoScreen.texture = videoPlayer.texture;
        videoPlayer.Play();
    }
}
