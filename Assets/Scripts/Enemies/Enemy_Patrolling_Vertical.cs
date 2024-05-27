using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet a l'ennemi de changer de gravit� sur un temps defini 
public class Enemy_Patrolling_Vertical : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Detection")]
    private bool top;

    public void Update()
    {
        if (top)
        {
            StartCoroutine(gravityDesactivated());
        }

        if (!top)
        {
            StartCoroutine(gravityActivated());
        }
    }

    // Permet de desactiver la gravite au bout d'un certain temps
    public IEnumerator gravityDesactivated()
    {
        rb.gravityScale = -10;
        yield return new WaitForSeconds(2);
        top = false;
    }

    // Permet d'activer la gravite au bout d'un certain temps
    public IEnumerator gravityActivated()
    {
        rb.gravityScale = 10;
        yield return new WaitForSeconds(2);
        top = true;
    }

    // Permet de tuer le Joueur s'il rentre en contact avec l'ennemi
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Controller playerHealth = other.transform.GetComponent<Player_Controller>();
            playerHealth.isDead = true;
        }
    }
}