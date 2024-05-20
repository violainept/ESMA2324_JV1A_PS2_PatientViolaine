using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player_Controller playerController = collision.transform.GetComponent<Player_Controller>();
            playerController.ActivateGravity();
        }
    }
}
