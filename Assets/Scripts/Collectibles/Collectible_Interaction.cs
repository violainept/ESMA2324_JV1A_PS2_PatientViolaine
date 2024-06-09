using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Interaction : MonoBehaviour

    // Permet de faire apparaitre un texte lorsque le Joueur s'approche puis un Collectible de Souvenir s'il interagit avec
{
    [Header("GameObject")]
    public Animator anim;

    [Header("Detection")]
    private bool playerIsInsideTrigger = false;
    public bool isCollected = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerIsInsideTrigger)
        {
            isCollected = true;
        }
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
