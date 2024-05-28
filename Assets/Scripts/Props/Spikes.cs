using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de tuer le Joueur s'il rentre en contact avec

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player_Controller playerHealth = other.transform.GetComponent<Player_Controller>();
            playerHealth.isDead = true;
        }
    }
}
