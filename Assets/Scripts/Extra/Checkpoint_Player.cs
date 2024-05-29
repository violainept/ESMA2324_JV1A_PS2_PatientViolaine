using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de garder la position du Joueur pour le refaire apparaitre au meme endroit
public class Checkpoint_Player : MonoBehaviour
{
    [Header("Autre")]
    private Transform playerSpawn;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    // Si le Joueur entre en contact, le checkpoint garde la position en memoire
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerSpawn.position = transform.position;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

}
