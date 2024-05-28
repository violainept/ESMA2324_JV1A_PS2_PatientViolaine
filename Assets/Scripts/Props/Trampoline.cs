using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire rebondir le Joueur lorsqu'il rentre en contact avec
public class Trampoline : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player_Controller player = collision.transform.GetComponent<Player_Controller>();
            player.ChangeGravity();
        }
    }
}