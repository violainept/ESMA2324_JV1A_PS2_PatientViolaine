using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Movements : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //
    
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    // variables deplacements
    private float horizontal;
    private float speed = 5f;

    // variables inverser gravité
    private float gravityNumber = 3; // TEST
    public float currentGravityCounter;
    private float minGravityCounter = 0;
    private float maxGravityCounter;

    // variables dash
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    public float dashCounter;
    public bool canDash;
    private bool isDashing;
    [SerializeField] private TrailRenderer tr;

    // variables changer de sens
    private bool isFacingRight = true;
    private bool top;

    // variables contact avec le sol
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    // variables accroupi
    [SerializeField] public Sprite standing;
    [SerializeField] public Sprite crouching;
    [SerializeField] private Vector2 standingSize;
    [SerializeField] private Vector2 crouchingSize;
    [SerializeField] private Vector2 standingOffset;
    [SerializeField] private Vector2 crouchingOffset;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        boxCollider.size = standingSize;
        spriteRenderer.sprite = standing;
        standingSize = boxCollider.size;

        maxGravityCounter = gravityNumber;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isGrounded())
        {
            maxGravityCounter = gravityNumber;
            currentGravityCounter = maxGravityCounter;
        }

        if (currentGravityCounter > (minGravityCounter - 1))
        {
            if (isDashing)
            {
                return;
            }

            Crouching();
            ChangeGravityV2();
            Flip();
            Dash(); // TEST

        }
        else
        {
            Debug.Log("Dead");
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
    private void Moving()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    // ----------------------------------------------------------------------------------- Mecanique : Accroupi ----------------------------------------------------------------------------------- //
    private void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.C) && isGrounded())
        {
            spriteRenderer.sprite = crouching;
            boxCollider.size = crouchingSize;
            boxCollider.offset = crouchingOffset;
        }

    // ----------------------------------------------------------------------------------- Mecanique : Debout ----------------------------------------------------------------------------------- //

        if (Input.GetKeyUp(KeyCode.C))
        {
            spriteRenderer.sprite = standing;
            boxCollider.size = standingSize;
            boxCollider.offset = standingOffset;
        }
    }

   // ----------------------------------------------------------------------------------- PowerUp 1 : Inverser gravité ----------------------------------------------------------------------------------- //
    private void ChangeGravityV1() // Durée dans le temps, se recharge au sol
    {
        if (Input.GetKeyDown("space"))
        {
            
            rb.gravityScale *= -1;
            Rotation();
        }
    }    private void ChangeGravityV2() // Durée limitée, se recharge lors d'un checkpoint
    {
        if (Input.GetKeyDown("space") && isGrounded())
        {
            if (currentGravityCounter > (minGravityCounter - 1))
            {
                currentGravityCounter -= 1;
            }

            rb.gravityScale *= -1;
            Rotation();
        }
    }

    // ----------------------------------------------------------------------------------- PowerUp 2 : Dash ----------------------------------------------------------------------------------- //
   
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

    // ----------------------------------------------------------------------------------- GameObject : Contact Sol ----------------------------------------------------------------------------------- //

    public bool isGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Change de sens (gauche/droite) ----------------------------------------------------------------------------------- //
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


