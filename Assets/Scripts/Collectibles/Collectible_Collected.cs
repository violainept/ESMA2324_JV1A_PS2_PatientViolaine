using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collectible_Collected : MonoBehaviour
{
    public bool isCollected = false;
    // public AudioClip sound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollected = true;
            // AudioManager.instance.PlayClipAt(sound, transform.position);
            Destroy(gameObject);
        }
    }
}
