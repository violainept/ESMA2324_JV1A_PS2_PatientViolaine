using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Interact : MonoBehaviour
{
    public GameObject panel;
    public Player_Controller player;

    private Animator anim;


    private void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player_Controller)) as Player_Controller;

    }
    private void Update()
    {
        if (player.isInteracting == true)
        {
            Interaction();
        }

    }

    private void Interaction()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {

        }
    }
}
