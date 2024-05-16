using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movements : MonoBehaviour
{
    [Header("Configurations")]

    public Sprite standing;
    public Sprite crouching;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private float horizontal;
    private float speed = 5f;
    private float jumpForce = 5f;

    private bool isCrouched = false;

    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 standingSize;
    [SerializeField] private Vector2 crouchingSize;
    [SerializeField] private Vector2 standingOffset;
    [SerializeField] private Vector2 crouchingOffset;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = standingSize;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = standing;

        standingSize = boxCollider.size;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Moving();
        Jumping();
        Crouching();
        ChangeGravity();
    }

    // ----------------------------------------------------------------------------------- Deplacements ----------------------------------------------------------------------------------- //
    private void Moving()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }
    
    // ----------------------------------------------------------------------------------- Saut ----------------------------------------------------------------------------------- //
    private void Jumping()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded() && !isCrouched)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    // ----------------------------------------------------------------------------------- Accroupis ----------------------------------------------------------------------------------- //
    private void Crouching()
    {
        // Si le Joueur appuie sur le bouton C et qu'il est en contact avec un sol
        if (Input.GetKeyDown(KeyCode.C) && IsGrounded())
        {
            isCrouched = true;
            spriteRenderer.sprite = crouching;
            boxCollider.size = crouchingSize;
            boxCollider.offset = crouchingOffset;
        }

    // ----------------------------------------------------------------------------------- Debout ----------------------------------------------------------------------------------- //
        if (Input.GetKeyUp(KeyCode.C))
        {
            isCrouched = false;
            spriteRenderer.sprite = standing;
            boxCollider.size = standingSize;
            boxCollider.offset = standingOffset;
        }
    }

    private void ChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.gravityScale = -4;
            // A ajouter > %
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }
}


