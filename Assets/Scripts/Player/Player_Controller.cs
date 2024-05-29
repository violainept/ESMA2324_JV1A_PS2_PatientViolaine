using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Proprietes et Variables ----------------------------------------------------------------------------------- //

    [Header("Autre")]
    private Transform playerSpawn;

    [Header("GameObject")]
    public Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    [Header("Mouvements")]
    private float horizontal;
    private float speed = 10f;

    [Header("Inverser gravité")]
    [SerializeField] private float currentGravity;
    [SerializeField] private bool canTriggerGravity = true;
    private float maxGravity = 2;

    [Header("Mort")]
    public bool isDead;
    private float timerDelayDeath;
    private Vector2 originalScale;

    [Header("Contact au Sol")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Se retourne")]
    private bool isFacingRight = true;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalScale = new Vector2(0.5937178f, 0.5937178f);
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (isDead)
        {
            Die();
        }

        if (!isDead)
        {
            if (isGrounded())
            {
                currentGravity = maxGravity;
                canTriggerGravity = true;
            }

            Flip();
            EnableGravity();
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

    // ----------------------------------------------------------------------------------- GameObject : Mort ----------------------------------------------------------------------------------- //
    // Permet au Joueur de mourir et de respawn

    public void Die()
    {
        isDead = true;
        //animator.SetTrigger("Die");
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector3.zero;
        boxCollider.enabled = false;
        timerDelayDeath += Time.deltaTime;

        if (timerDelayDeath > 2)
        {
            timerDelayDeath = 0;
            Respawn();
        }
    }

    // ----------------------------------------------------------------------------------- GameObject : Respawn ----------------------------------------------------------------------------------- //
    // Permet de refaire apparaitre le joueur
    public void Respawn()
    {
        transform.position = playerSpawn.position;
        //animator.SetTrigger("Respawn");
        rb.bodyType = RigidbodyType2D.Dynamic;
        transform.localScale = originalScale;
        boxCollider.enabled = true;
        isDead = false;
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


