using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script permettant de faire tomber la plateforme si le Joueur rentre en contact avec

public class Platform_Falling : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propri�t�s et Variables ----------------------------------------------------------------------------------- //

    private float fallDelay = 0.5f;
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    // Si le Joueur est d�tect�, lance la fonction de tomber
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            StartCoroutine(Fall());
        }
    }

    // ----------------------------------------------------------------------------------- Tombe ----------------------------------------------------------------------------------- //

    // Changement du rigidbody de la plateforme en dynamique afin qu'au bout de quelques secondes elle tombe et se d�truit
    // Permet de faire tomber la plateforme apr�s quelques secondes (+ detruire afin d'eviter tout probleme avec d'autres gameobjects pr�sents)
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
