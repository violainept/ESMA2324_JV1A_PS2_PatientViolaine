using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet a l'ennemi de changer de gravité sur un temps defini 
public class Enemy_Patrolling_Vertical : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator anim;

    [Header("Detection")]
    private bool top;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
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
        rb.gravityScale = -3;
        yield return new WaitForSeconds(2);
        top = false;
    }

    // Permet d'activer la gravite au bout d'un certain temps
    public IEnumerator gravityActivated()
    {
        rb.gravityScale = 3;
        yield return new WaitForSeconds(2);
        top = true;
    }

    // Permet de tuer le Joueur s'il rentre en contact avec l'ennemi
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player_Controller player = other.transform.GetComponent<Player_Controller>();
            player.Die();
        }
    }
    private void Rotation()
    {
        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
    }
}