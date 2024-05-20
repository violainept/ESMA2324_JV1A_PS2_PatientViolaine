using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //

    public bool canUseGravity = true;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private float horizontal;
    private float speed = 5f;
    private float gravityNumber = 3; // TEST
    private float minGravityCounter = 0;
    private float maxGravityCounter;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;

    private bool isDashing;
    private bool isDead = false;
    private bool isFacingRight = true;
    private bool top;

    [SerializeField] private bool canDash;

    [SerializeField] private float currentGravityCounter;
    [SerializeField] private float dashCounter;

    [SerializeField] private TrailRenderer tr;
    [SerializeField] private ParticleSystem ps;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    // Initialisation
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        maxGravityCounter = gravityNumber;
    }

    // Commencement
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            maxGravityCounter = gravityNumber;
            currentGravityCounter = maxGravityCounter;
        }

        // SI Cas InverseGravityV2
        if (currentGravityCounter <= (minGravityCounter - 1))
        {
            Die();
        }


            if (!isDead)
        {
            if (isDashing)
            {
                return;
            }

            ChangeGravity();
            FlipDown();
            Dash(); // TEST

        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (currentGravityCounter > (minGravityCounter - 1))
        {
            Moving();
        }
    }

    // ----------------------------------------------------------------------------------- Mecanique : Deplacements ----------------------------------------------------------------------------------- //
    
    // Permet au Joueur de se déplacer
    private void Moving()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // ----------------------------------------------------------------------------------- PowerUp 1 : Inverser gravité ----------------------------------------------------------------------------------- //
   
    // Permet au Joueur d'inverser la gravité sur un temps limité
    private void ChangeGravityV1() // Durée dans le temps, se recharge au sol
    {
        if (Input.GetKeyDown("space"))
        {
            if (currentGravityCounter > (minGravityCounter - 1))
            {
                currentGravityCounter -= 1;
            }

            rb.gravityScale *= -1;
            Rotation();
        }
    }

    private void ChangeGravity()
    {
        if (Input.GetKeyDown("space") && isGrounded() && canUseGravity)
        {
            ActivateGravity();
        }
    }
     public void ActivateGravity() // Durée limitée, se recharge lors d'un checkpoint
     {
            rb.gravityScale *= -1;
            Rotation();
     }

    // ----------------------------------------------------------------------------------- PowerUp 2 : Dash ----------------------------------------------------------------------------------- //
   
    // Permet au Joueur de réaliser un Dash lorsqu'il est dans les airs et qui se recharge au sol
    private void Dash() // Dash limité, utilisation unique lorsque la gravité inversée est activée.
    {

        if (isGrounded())
        {
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash && !isGrounded())
        {
            StartCoroutine(DashCoroutine());
        }
    }

    private  IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    // ----------------------------------------------------------------------------------- GameObject : Mort du Joueur ----------------------------------------------------------------------------------- //

    // Permet au Joueur de mourir
    public void Die()
    {
        isDead = true;
        ps.Play();
        Debug.Log("Player is dead.");
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
    private void FlipDown()
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Change de sens (gauche/droite) par rapport au plafond ----------------------------------------------------------------------------------- //

    // Permet de tourner le Joueur vers la gauche et la droite lorsqu'il est à l'envers
    private void FlipUp()
    {

        if (isFacingRight && horizontal > 0f || !isFacingRight && horizontal < 0f)
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
        if (top == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
            top = !top;
    }
}


