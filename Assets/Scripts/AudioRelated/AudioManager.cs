using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Audio[] AudioClip;

    private void Awake()
    {
        foreach(Audio a in AudioClip)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.Sound;
            a.source.volume = a.volume;
        }
    }

    public void playAudio(string name)
    {
        foreach(Audio a in AudioClip)
        { 
            if (a.m_Name == name)
            {
                a.source.Play();
            }
        }
    }
}
