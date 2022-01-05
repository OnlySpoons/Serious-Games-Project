using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] m_sounds;

    public static AudioManager m_instance;

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in m_sounds)
        {
            s.m_source = gameObject.AddComponent<AudioSource>();
            s.m_source.clip = s.m_clip;
            s.m_source.loop = s.m_loop;
            s.m_source.outputAudioMixerGroup = s.m_group;
        }
    }

    void Start()
    {
        Play("MenuMusic");
    }

    public void Play(string _name)
    {
       Sound s = Array.Find(m_sounds, sound => sound.m_name == _name);

        if(s == null)
        {
            //Debug
            Debug.LogWarning("Sound: " + name + " not found!");
            
            return;            
        }

        s.m_source.volume = s.m_volume;
        s.m_source.pitch = s.m_pitch;

        s.m_source.Play();
    }

    public void StopPlaying(string _sound)
    {
        Sound s = Array.Find(m_sounds, item => item.m_name == _sound);
        if (s == null)
        {
            //Debug
            Debug.LogWarning("Sound: " + name + " not found!");

            return;
        }

        //TESTING
        //s.m_source.volume = s.m_volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        //s.m_source.pitch = s.m_pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.m_source.Stop();
    }
}
