using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Interact : MonoBehaviour
{
    public GameObject panelObject;
    public Player_Controller player;
    public Animator anim;

    private bool panelIsActivated = false;
    private bool playerIsInsideTrigger = false;


    private void Start()
    {
        player = GameObject.FindObjectOfType(typeof(Player_Controller)) as Player_Controller;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInsideTrigger)
        {
            Interaction();
        }

        if (Input.GetMouseButtonDown(0) && panelIsActivated)
        {
            panelIsActivated = false;
            panelObject.SetActive(false);
        }
    }

    private void Interaction()
    {
        panelIsActivated = true;
        panelObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Visible();
            playerIsInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Invisible();

            if (panelIsActivated)
            {
                panelObject.SetActive(false);
            }

            playerIsInsideTrigger = false;
        }
    }

    private void Visible()
    {
        anim.SetBool("objectIsVisible", true);
    }

    private void Invisible()
    {
        anim.SetBool("objectIsVisible", false);
    }

}
