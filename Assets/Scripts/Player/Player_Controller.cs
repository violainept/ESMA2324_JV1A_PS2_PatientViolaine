using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Proprietes et Variables ----------------------------------------------------------------------------------- //

    // Other objects
    private Player_Health playerHealth;

    // GameObject
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    // Mouvements
    private float horizontal;
    private float speed = 10f;

    // Gravite
    private float maxGravity = 2;
    [SerializeField] private float currentGravity;
    [SerializeField] private bool canTriggerGravity = true;

    // Interagir
    public bool isInteracting = false;

    // Contact avec le sol 
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    // Flip
    private bool isFacingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            currentGravity = maxGravity;
            canTriggerGravity = true;
        }

        Flip();
        EnableGravity();
    }

    private void FixedUpdate()
    {

        Moving();
    }

    // ----------------------------------------------------------------------------------- Mecanique : Deplacements ----------------------------------------------------------------------------------- //
    // Permet au Joueur de se déplacer
    private void Moving()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }


    // ----------------------------------------------------------------------------------- Mecanique principale : Inverser gravité ----------------------------------------------------------------------------------- //
    // Permet au Joueur de changer la gravité de façon limitée (maximum de 2 fois)
    public void EnableGravity()
    {
        if (Input.GetKeyDown("space") && canTriggerGravity)
        {
            if (currentGravity > 0)
            {
                currentGravity -= 1;
                ChangeGravity();
            }

            if (currentGravity <= 0)
            {
                canTriggerGravity = false;
            }
        }
    }
    
    public void ChangeGravity()
    {
        rb.gravityScale *= -1;
        Rotation();
     }

    // ----------------------------------------------------------------------------------- GameObject : Contact Sol ----------------------------------------------------------------------------------- //
    // Permet de vérifier si le Joueur entre en contact avec un sol
    public bool isGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Change de sens (gauche/droite) par rapport au sol ----------------------------------------------------------------------------------- //
    // Permet de tourner le Joueur vers la gauche et la droite lorsqu'il est droit
    
    private void Flip()
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Change de sens (bas/haut) ----------------------------------------------------------------------------------- //
    // Permet au Joueur d'avoir une rotation lorsqu'il utilise la mécanique d'inverser la gravité
    private void Rotation()
    {

        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
    }
}


