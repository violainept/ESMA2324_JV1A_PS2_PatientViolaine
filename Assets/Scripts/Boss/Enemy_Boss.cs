using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;


public class Enemy_Boss : MonoBehaviour
{
    // Autres GameObject/transform/scripts
    public Player_Controller playerController;
    public Object_UsingGravity_Boss objectBoss;
    GameObject objectGO; // "objectGO" signifie "objectGameObject"
    public Transform player;

    // GameObject
    public Rigidbody2D rb;

    // Timers
    private float timerSpecialAttack;
    private float timerCooldown;
    private float timerGravity;
    private bool cooldown = false;

    // Vie
    private float health = 9;

    // Se retourner
    public bool isFlipped = false;

    // Courir 
    public float speed;
   
    // Attaquer
    public Vector3 attackOffset;
    private float attackRange = 1f;
    public LayerMask attackMask;

    // Dash
    private float dashingPower = 50f;
    private float dashingTime = 0.2f;
    private bool isDashing;
    [SerializeField] private bool canDash;
    [SerializeField] private TrailRenderer tr;

    // Contact avec le sol 
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    private void Update()
    {
        timerSpecialAttack += Time.deltaTime;
        timerCooldown += Time.deltaTime;
        timerGravity += Time.deltaTime;


        if (timerSpecialAttack < 20 && !cooldown)
        {
            Run();
        }

        if (timerCooldown >= 5)
        {
            cooldown = true;
            StartCoroutine(Cooldown());
        }

        if (timerGravity >= 10)
        {
            rb.isKinematic = false;
            rb.gravityScale *= -1;

            if (isGrounded())
            {
                rb.isKinematic = true;
            }

            timerGravity = 0;
        }

        if (timerSpecialAttack >= 20)
        {
            SpecialAttack();
        }
    }

    // Attente obligatoire entre les actions du Boss
    private IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(1);
        cooldown = false;
    }

    // Permet de faire des degats au Boss
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            Destroy(objectGO);
            TakeDamage();
        }
    }

    // Permet au Boss de prendre des degats
    public void TakeDamage()
    {
        health -= 1;
        if (health <= 0)
        {
            Die();
        }
    }

    // Permet de tuer le Boss
    private void Die()
    {
        Debug.Log("Dead");
    }

    // Permet au Boss de regarder dans la direction du Joueur
    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    // Permet au Boss de courir
    private void Run()
    {
        LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            Attack();
        }
    }

    // Permet au Boss d'attaquer
    private void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        
        StartCoroutine(Dash());

        if (colInfo != null)
        {
            Debug.Log("Damage player");
            StartCoroutine(Cooldown());
        }
    }

    // Permet au Boss de faire un Dash (attaque)
    private IEnumerator Dash()
    {
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
    }

    // Permet au Boss de realiser une attaque speciale (changer la gravite du Joueur)
    private void SpecialAttack()
    {
        playerController.ChangeGravity();
        timerSpecialAttack = 0;
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }


}
