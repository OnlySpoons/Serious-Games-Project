using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    public RawImage img;
    public VideoPlayer vid;
    //public AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        vid.Prepare();
        int i = 0;
        while(!vid.isPrepared)
        {
            yield return i;
        }
        img.texture = vid.texture;
        vid.Play();
        //aud.Play();
    }
}
