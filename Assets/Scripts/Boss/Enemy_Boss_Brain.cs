using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;


public class Enemy_Boss_Brain : MonoBehaviour
{
    [Header("Activer Boss")]
    public bool isActivate = false;

    [Header("Autres")]
    public Player_Controller playerController;
    private Object_UsingGravity_Boss objectBoss;
    private Object_Spawner_Boss wall;
    public Transform player;
    public GameObject objectGO; // "objectGO" signifie "objectGameObject"

    [Header("Lien Script")]
    private Enemy_Boss_Attack enemyAttack;

    [Header("GameObject")]
    public Rigidbody2D enemyRB;
    private Vector2 originalPosition;

    [Header("Timers")]
    public float timerSpecialAttack;
    public float timerCooldown;
    private float timerGravity;
    private bool cooldown = false;

    [Header("Vie")]
    private float health = 3;

    [Header("Se retourne")]
    public bool isFlipped = false;

    [Header("Cours")]
    public float speed;

    [Header("Contact Sol")]
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;


    private void Start()
    {
        objectBoss = objectGO.GetComponent<Object_UsingGravity_Boss>();
        originalPosition = new Vector2(0, 0);
    }

    // Si le Joueur est mort, reset les timers et positions du Boss. Sinon, si le Boss est active, commence le combat.
    private void Update()
    {
        if (playerController.Restart == true)
        {
            Reset();
        }

        if (isActivate)
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
                enemyRB.isKinematic = false;
                enemyRB.gravityScale *= -1;

                if (isGrounded())
                {
                    enemyRB.isKinematic = true;
                }

                timerGravity = 0;
            }

            if (timerSpecialAttack >= 20)
            {
                enemyAttack.SpecialAttack();
            }
        }
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

    // Si contact avec un Object, prend des degats
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Object"))
        {
            objectBoss.Contact();
            wall.canDestroy = true;
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

    // Permet au Boss de courir
    private void Run()
    {
        LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, enemyRB.position.y);
        Vector2 newPos = Vector2.MoveTowards(enemyRB.position, target, speed * Time.fixedDeltaTime);
        enemyRB.MovePosition(newPos);

        if (Vector2.Distance(player.position, enemyRB.position) <= enemyAttack.attackRange)
        {
            enemyAttack.Attack();
        }
    }

    // Permet de verifier si contact avec le sol
    public bool isGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }

    // Attente entre les actions du Boss
    public IEnumerator Cooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(1);
        cooldown = false;
    }

    // Permet de reset le Boss
    private void Reset()
    {
        isActivate = false;
        transform.position = originalPosition;
        timerCooldown = 0;
        timerGravity = 0;
        timerSpecialAttack = 0;
        health = 3;
    }
}
