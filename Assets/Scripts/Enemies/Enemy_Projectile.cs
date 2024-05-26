using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Projectile : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //

    public GameObject player;
    private Rigidbody2D rb;

    public float force;
    private float timer;


    // Permet de faire bouger le projectile
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Permet de detruire le projectile au bout d'une certaine duree
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }

    // Lorsque le Joueur entre en contact avec la boxCollider, il meurt et le projectile est detruit
    void OnTriggerEnter2D(Collider2D other)
    {
       if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player_Health>().isDead = true;
            Destroy(gameObject);
        }
    }
}
