using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Boss_Brain : MonoBehaviour
{
    [Header("Autres")]
    public Player_Controller playerController;
    public Spawner_Object_Boss objectSpawner;
    public Transform player;
    public Rigidbody2D playerRB; // "playerRB" signifie "playerRigidBody"
    private GameObject playerGO;
    public GameObject objectGO; // "objectGO" signifie "objectGameObject"
    private Object_Boss objectBoss;
    public GameObject panelCredits;

    [Header("GameObject")]
    public Rigidbody2D bossRB; // "bossRB" signifie "bossRigidBody"
    public SpriteRenderer graphics;
    public BoxCollider2D hitbox;
    public BoxCollider2D boxCollider;
    public Animator anim;
    private Boss_Attack bossAttack;
    private bool isInvincible;

    [Header("Cours")]
    public float speed;

    [Header("Attaques")]
    public bool useAttack;

    [Header("Vie")]
    private float health = 9;

    [Header("Se retourne")]
    public bool isFlipped = false;

    private void Awake()
    {
        playerGO = GameObject.FindGameObjectWithTag("Player");
        playerRB = playerGO.GetComponent<Rigidbody2D>();
        player = playerGO.transform;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        objectBoss = objectGO.GetComponent<Object_Boss>();
        bossAttack = GetComponent<Boss_Attack>();
    }

    // Si le Joueur est mort, reset les timers et positions du Boss. Sinon, si le Boss est active, commence le combat.
    private void Update()
    {
        anim.SetBool("Activate", true);

        if (!useAttack && !isInvincible)
        {
            if (playerRB.gravityScale == 60)
            {
                Run();
            }

            if (playerRB.gravityScale == -60)
            {
                anim.SetBool("Shooting", true);
                bossAttack.Shooting();
            }

            else
            {
                anim.SetBool("Shooting", false);
            }
        }
    }

    // Permet au Boss de courir
    private void Run()
    {
        LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, bossRB.position.y);
        Vector2 newPos = Vector2.MoveTowards(bossRB.position, target, speed * Time.fixedDeltaTime);
        bossRB.MovePosition(newPos);

        if (Vector2.Distance(player.position, bossRB.position) <= bossAttack.attackRange)
        {
            bossAttack.Attack();
        }
    }

    // Permet au Boss de prendre des degats
    public void TakeDamage()
    {
        anim.SetBool("Hurting", true);
        bossRB.gravityScale = 10;

        health -= 1;
        Invincibility();

        if (health <= 0)
        {
            Die();
            return;
        }
    }

    private void Invincibility()
    {
        StartCoroutine(InvincibilityTimer());
    }

    private IEnumerator InvincibilityTimer()
    {
        isInvincible = true;
        bossRB.isKinematic = true;
        boxCollider.enabled = false;
        hitbox.enabled = false;
        yield return new WaitForSeconds(2);
        bossRB.isKinematic = false;
        boxCollider.enabled = true;
        hitbox.enabled = true;
        anim.SetBool("Hurting", false);
        isInvincible = false;
    }

    // Permet de tuer le Boss
    private void Die()
    {
        anim.SetBool("Dying", true);
    }

    private void DestroyBoss()
    {
        panelCredits.SetActive(true);
        Destroy(gameObject);
    }

    // Permet au Boss de regarder dans la direction du Joueur
    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    // Permet de changer de sens le Boss
    private void Rotation()
    {
        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
    }
}
