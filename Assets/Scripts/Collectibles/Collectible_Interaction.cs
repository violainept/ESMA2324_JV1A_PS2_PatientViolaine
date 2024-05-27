using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Interaction : MonoBehaviour

    // Permet de faire apparaitre un texte lorsque le Joueur s'approche puis un Collectible de Souvenir s'il interagit avec
{
    [Header("GameObject")]
    public GameObject panelObject;
    public Animator anim;

    [Header("Detection")]
    private bool panelIsActivated = false;
    private bool playerIsInsideTrigger = false;

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

    // Permet de faire apparaitre le Collectible de Souvenir
    private void Interaction()
    {
        panelIsActivated = true;
        panelObject.SetActive(true);
    }

    // Permet de rendre visible avec une animation le texte lorsque le Joueur s'approche
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Visible();
            playerIsInsideTrigger = true;
        }
    }

    // Permet de rendre invisible avec une animation le texte lorsque le Joueur s'eloigne
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

    // Permet de rendre visible le texte
    private void Visible()
    {
        anim.SetBool("objectIsVisible", true);
    }

    // Permet de rendre invisible le texte
    private void Invisible()
    {
        anim.SetBool("objectIsVisible", false);
    }

}
