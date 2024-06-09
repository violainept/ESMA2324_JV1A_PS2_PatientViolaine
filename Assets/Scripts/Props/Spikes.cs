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
            Player_Death player = other.transform.GetComponent<Player_Death>();
            player.isDead = true;
        }
    }
}
