using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioClip clip;

    [HideInInspector]
    public AudioSource source;

    // public string name;

    [Range(0f, 1f)]
    public float volume = 1.0f;

    public bool loop = false;
}
