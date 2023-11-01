using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] musicSounds,sfxSounds; 
    public AudioSource musicSource,sfxSource;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(musicSounds,x => x.nameClip == name);

        if (sound == null) 
        {
            Debug.LogError("Sound not found");
        }
        else
        {
            musicSource.clip = sound.clip;
            musicSource.Play();
        }

    }
    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(sfxSounds, x => x.nameClip == name);

        if (sound == null)
        {
            Debug.LogError("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(sound.clip);
        }

    }
}
