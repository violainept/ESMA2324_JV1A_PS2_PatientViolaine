using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    public AudioClip background;
    public AudioClip bossTheme;

    public void PlayMusic(AudioClip source)
    {
        musicSource.clip = source;
        musicSource.Play();
    }
}
