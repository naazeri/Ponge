using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] bounceSounds;
    public Sound explosionSound;
    private int lastSoundIndex = 0;

    void Awake()
    {
        if (!UseSingleton())
            return;

        foreach (var sound in bounceSounds)
        {
            AddAudioSource(sound);
        }

        AddAudioSource(explosionSound);
    }

    private bool UseSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }

        DontDestroyOnLoad(gameObject); // keep AudioManager on scene change

        return true;
    }

    private void AddAudioSource(Sound sound)
    {
        sound.source = gameObject.AddComponent<AudioSource>();
        sound.source.clip = sound.clip;
        sound.source.volume = sound.volume;
        // sound.source.pitch = sound.pitch;
        sound.source.loop = sound.loop;
    }


    // public void Play(string name)
    // {
    //     var s = Array.Find(bounceSounds, (sound) => sound.name == name);

    //     if (s == null)
    //     {
    //         Debug.LogWarning($"Sound: {name} not found!");
    //         return;
    //     }

    //     s.source.Play();
    // }

    public void PlayBouncSound()
    {
        var sound = bounceSounds[lastSoundIndex];

        if (sound == null)
        {
            Debug.LogWarning($"Sound at index: {lastSoundIndex} not found!");
            return;
        }

        sound.source.Play();

        lastSoundIndex = (lastSoundIndex + 1 >= bounceSounds.Length) ? 0 : lastSoundIndex + 1;
    }

    public void PlayExplosionSound()
    {
        explosionSound.source.Play();
    }

}
