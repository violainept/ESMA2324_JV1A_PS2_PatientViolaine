using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire tomber la plateforme si le Joueur rentre en contact avec
public class Platform_Falling : MonoBehaviour
{
    [Header("GameObject")]
    private Animator anim;

    [Header("Timers")]
    private float destroyDelay = 2f;

    [SerializeField] private Rigidbody2D rb;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Si le Joueur est détecté, la plateforme tombe
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Falling");
        }

        if (collision.gameObject.CompareTag("DeadlyProps"))
        {
            Destroy(gameObject);
        }
    }

    public void Falling()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }
}
