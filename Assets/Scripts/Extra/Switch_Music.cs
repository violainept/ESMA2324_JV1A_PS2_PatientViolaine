using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Switch_Music : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    Audio_Manager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio_Manager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.backgroundMusic = false;
            audioManager.bossThemeMusic = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioManager.bossThemeMusic = false;
            audioManager.backgroundMusic = false;
        }
    }
}
