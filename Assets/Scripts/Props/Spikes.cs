using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player_Health playerHealth = other.transform.GetComponent<Player_Health>();
            playerHealth.isDead = true;
        }
    }
}
