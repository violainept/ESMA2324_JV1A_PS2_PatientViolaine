using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //

    // GameObject
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    // Mouvements
    private float horizontal;
    private float speed = 10f;

    // Gravite
    public float currentGravity;
    public float maxGravity;
    public bool canTriggerGravity = true;

    // Dash
    private float dashingPower = 50f;
    private float dashingTime = 0.2f;
    private bool isDashing;
    [SerializeField] private bool canDash;

    // Interagir
    public bool isInteracting = false;

    // Mort
    private bool isDead = false;

    // Contact avec le sol 
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    // Flip
    private bool isFacingRight = true;

    // Effets
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private ParticleSystem ps;


    // Initialisation
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Commencement
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (!isDead)
        {
                    if (isGrounded())
        {
            currentGravity = maxGravity;
            canTriggerGravity = true;
        }

            if (isDashing)
            {
                return;
            }

            Interact();
            ActivateGravity();
            Flip();
            Dash();

        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            if (isDashing)
            {
                return;
            }
            Moving();
        }
    }

    // ----------------------------------------------------------------------------------- Mecanique : Deplacements ----------------------------------------------------------------------------------- //
    
    // Permet au Joueur de se déplacer
    private void Moving()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // ----------------------------------------------------------------------------------- Mecanique : Interagir  ----------------------------------------------------------------------------------- //

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
        }
    }

    // ----------------------------------------------------------------------------------- PowerUp 1 : Inverser gravité ----------------------------------------------------------------------------------- //

    // Permet au Joueur de changer la gravité dans un temps limité

    // Notes : possibilite une (gravite se recharge au contact sol) et possibilite deux (gravite se recharge a un checkpoint)

    public void ActivateGravity()
    {
        if (Input.GetKeyDown("space") && canTriggerGravity)
        {
            ChangeGravity();
        }
    }
    
    public void ChangeGravity()
    {
        if (currentGravity > 0)
        {
            currentGravity -= 1;
            rb.gravityScale *= -1;
            Rotation();
        }

        if (currentGravity <= 0)
        {
            canTriggerGravity = false;
        }
     }


    // ----------------------------------------------------------------------------------- PowerUp 2 : Dash ----------------------------------------------------------------------------------- //
    // Permet au Joueur de réaliser un Dash
    private void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private  IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        canDash = true;
    }

    // ----------------------------------------------------------------------------------- GameObject : Mort du Joueur ----------------------------------------------------------------------------------- //

    // Permet au Joueur de mourir
    public void Die()
    {
        Debug.Log("Player is dead.");
        isDead = true;
        ps.Play();
        //animator.SetTrigger(dead);
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


