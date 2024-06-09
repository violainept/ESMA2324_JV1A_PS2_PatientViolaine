using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip bossTheme;
    public AudioClip background3;

    public bool backgroundMusic = true;
    public bool bossThemeMusic;

    private void Update()
    {
        if (backgroundMusic)
        {
            musicSource.clip = background;
            musicSource.Play();
        }

        if (bossThemeMusic)
        {
            musicSource.clip = bossTheme;
            musicSource.Play();
        }
    }

    public void PlaySFX(AudioClip sfxClip)
    {
        SFXSource.PlayOneShot(sfxClip);
    }
}
