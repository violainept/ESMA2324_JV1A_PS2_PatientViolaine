using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Proprietes et Variables ----------------------------------------------------------------------------------- //

    [Header("GameObject")]
    public Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Animations")]
    public Animator anim;

    [Header("Mouvements")]
    private float horizontal;
    private float speed = 10f;

    [Header("Saut")]
    public float jumpForce;

    [Header("Inverser Gravite")]
    public bool getPowerUp;
    private float gravityCount;
    private bool gravityUp;
    private bool gravityDown;

    [Header("Mort")]
    public bool isDead;
    private float timerDelayDeath;

    [Header("Contact au Sol")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Se retourner")]
    private bool isFacingRight = true;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

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
            gravityCount = 1; // Remettre à zero le compteur de gravité

            anim.SetBool("isJumping", false);
        }

        if (!isDead)
        {
            Jumping();
            Flip();

            anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

            if (getPowerUp)
            {
                EnableGravity();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
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


    // ----------------------------------------------------------------------------------- Mecanique : Saut ----------------------------------------------------------------------------------- //
    // Permet au Joueur de sauter
    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            if (rb.gravityScale == 60)
            {
                rb.gravityScale = 20;
            }

            if (rb.gravityScale == -60)
            {
                rb.gravityScale = -20;
            }

            if (rb.gravityScale == 20)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (rb.gravityScale == -20)
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
            }

            anim.SetBool("isJumping", true);
        }
    }

    // ----------------------------------------------------------------------------------- Mecanique principale : Inverser gravité ----------------------------------------------------------------------------------- //
    // Permet au Joueur de changer la gravité de façon limitée (maximum de 2 fois)
    public void EnableGravity()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (gravityCount == 1)
            {
                ChangeGravity();
            }
            gravityCount -= 1;
        }
    }
    
    public void ChangeGravity()
    {
        if (rb.gravityScale == 20)
        {
            anim.SetBool("usingGravity", true);
            rb.gravityScale = -60;
            Rotation();
            return;
        }

        if (rb.gravityScale == 60)
        {
            anim.SetBool("usingGravity", true);
            rb.gravityScale = -60;
            Rotation();
            return;
        }

        if (rb.gravityScale == -60)
        {
            anim.SetBool("usingGravity", false);
            rb.gravityScale = 60;
            Rotation();
            return;
        }

        if (rb.gravityScale == -20)
        {
            anim.SetBool("usingGravity", false);
            rb.gravityScale = 60;
            Rotation();
            return;
        }
     }

    // ----------------------------------------------------------------------------------- GameObject : Contact Sol ----------------------------------------------------------------------------------- //
    // Permet de vérifier si le Joueur entre en contact avec un sol
    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Change de sens (gauche/droite) par rapport au sol ----------------------------------------------------------------------------------- //
    // Permet de tourner le Joueur vers la gauche et la droite lorsqu'il est droit
    
    private void Flip()
    {

        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 currentScale = transform.localScale;
            currentScale.x *= -1f;
            transform.localScale = currentScale;
        }
    }

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Change de sens (bas/haut) ----------------------------------------------------------------------------------- //
    // Permet au Joueur d'avoir une rotation lorsqu'il utilise la mécanique d'inverser la gravité
    public void Rotation()
    {
        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
    }
}


