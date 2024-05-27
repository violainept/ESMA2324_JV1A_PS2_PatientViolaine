using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet a l'ennemi de changer de gravité sur un temps defini 
public class Enemy_Patrolling_Vertical : MonoBehaviour
{
    private bool top;

    [SerializeField] private Rigidbody2D rb;
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

    public IEnumerator gravityDesactivated()
    {
        rb.gravityScale = -10;
        yield return new WaitForSeconds(2);
        top = false;
    }

    public IEnumerator gravityActivated()
    {
        rb.gravityScale = 10;
        yield return new WaitForSeconds(2);
        top = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player_Controller playerHealth = other.transform.GetComponent<Player_Controller>();
            playerHealth.isDead = true;
        }
    }
}