using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propri�t�s et Variables ----------------------------------------------------------------------------------- //

    // Autres Scripts
    public Gravity_Bar gravityBar;

    // GameObject
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    // Mouvements
    private float horizontal;
    private float speed = 5f;

    // Dash
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private bool isDashing;
    [SerializeField] private bool canDash;
    [SerializeField] private float dashCounter;

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
        gravityBar = GameObject.FindObjectOfType(typeof(Gravity_Bar)) as Gravity_Bar;

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
            if (isDashing)
            {
                return;
            }

            ActivateGravity();
            FlipDown();
            Dash(); // A ajouter en tant que PowerUp

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
    
    // Permet au Joueur de se d�placer
    private void Moving()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // ----------------------------------------------------------------------------------- PowerUp 1 : Inverser gravit� ----------------------------------------------------------------------------------- //
     
    // Permet au Joueur de changer la gravit� dans un temps limit�

    // Notes : possibilite une (gravite se recharge au contact sol) et possibilite deux (gravite se recharge a un checkpoint)
    
    public void ActivateGravity()
    {
        if (Input.GetKeyDown("space"))
        {
            ChangeGravity();
        }
    }
    
    public void ChangeGravity()
    {
        rb.gravityScale *= -1;
        Rotation();
     }

    // ----------------------------------------------------------------------------------- PowerUp 2 : Dash ----------------------------------------------------------------------------------- //
   
    // Permet au Joueur de r�aliser un Dash lorsqu'il est dans les airs et qui se recharge au sol
    private void Dash() // Dash limit�, utilisation unique lorsque la gravit� invers�e est activ�e.
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
        Debug.Log("Player is dead.");
        isDead = true;
        ps.Play();
        //animator.SetTrigger(dead);
    }

    // ----------------------------------------------------------------------------------- GameObject : Contact Sol ----------------------------------------------------------------------------------- //

    // Permet de v�rifier si le Joueur entre en contact avec un sol
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

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Change de sens (bas/haut) ----------------------------------------------------------------------------------- //

    // Permet au Joueur d'avoir une rotation lorsqu'il utilise la m�canique d'inverser la gravit�
    private void Rotation()
    {

        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
    }
}


