using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet d'activer ou de desactiver l'obstacle 

public class Button : MonoBehaviour
{
    [Header("Autre")]
    public GameObject obstacle;
    public Animator obstacleAnim;

    [Header("GameObject")]
    public BoxCollider2D buttonCollider;
    public Animator buttonAnim;

    [Header("Taille")]
    private Vector2 originalSize;
    private Vector2 originalOffset;

    // Si le Joueur ou l'object se pose/est pose sur le GameObject, l'obstacle se desactive
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object"))
        {
            buttonAnim.SetBool("isPushed", true);
            originalSize = buttonCollider.size;
            originalOffset = buttonCollider.offset;
            buttonCollider.size = new Vector3(1.985535f, 0.3614942f, 0);
            buttonCollider.offset = new Vector3(0.003013611f, -0.15287f, 0);
            StartCoroutine(obstacleDesactivated());
        }

    }

    // Si le Joueur ou l'object s'en va/est retire, l'obstacle s'active de nouveau
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Object"))
        {
            buttonAnim.SetBool("isPushed", false);
            buttonCollider.size = originalSize;
            buttonCollider.offset = originalOffset;
            StartCoroutine(obstacleActivated());
        }
    }

    // Permet d'activer l'obstacle apres un moment
    private IEnumerator obstacleActivated()
    {
        obstacleAnim.SetBool("isVisible", true);
        yield return new WaitForSeconds(1);
    }

    // Permet de desactiver l'obstacle apres un moment
    private IEnumerator obstacleDesactivated()
    {
        obstacleAnim.SetBool("isVisible", false);
        yield return new WaitForSeconds(1);
    }
}
