using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire bouger une plateforme

public class Platform_Moving : MonoBehaviour
{
    [Header("Autre")]
    public Rigidbody2D playerRB;

    [Header("GameObject")]
    public Rigidbody2D platformRB;

    [Header("Points")]
    public Transform posA, posB;
    Vector3 targetPos;

    [Header("Vitesse")]
    public float speed;


    private void Start()
    {
        targetPos = posB.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.05f)
        {
            targetPos = posB.position;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    // Si le Joueur entre en contact, il devient enfant du GameObject
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = this.transform;
        }
    }

    // Si le Joueur s'en va, il n'est plus enfant du GameObject
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
