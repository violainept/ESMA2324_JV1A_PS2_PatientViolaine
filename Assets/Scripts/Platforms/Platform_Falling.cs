using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire tomber la plateforme si le Joueur rentre en contact avec
public class Platform_Falling : MonoBehaviour
{
    [Header("Timers")]
    private float fallDelay = 0.5f;
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    // Si le Joueur est détecté, la plateforme tombe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            {
            StartCoroutine(Fall());
        }
    }

    // Permet de faire tomber la plateforme après quelques secondes et de la detruire
    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
