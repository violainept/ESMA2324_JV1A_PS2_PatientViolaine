using System.Collections;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

public class Boss_Brain : MonoBehaviour
{
    [Header("Activer Boss")]
    public bool isActivate = false;

    [Header("Autres")]
    public Player_Controller playerController;
    public Spawner_Object_Boss objectSpawner;
    public Transform player;
    public Rigidbody2D playerRB; // "playerRB" signifie "playerRigidBody"
    public GameObject objectGO; // "objectGO" signifie "objectGameObject"
    public GameObject bossAwakeGO;
    private Object_Boss objectBoss;

    [Header("GameObject")]
    public Rigidbody2D bossRB; // "bossRB" signifie "bossRigidBody"
    public SpriteRenderer graphics;
    public BoxCollider2D hitbox;
    public BoxCollider2D boxCollider;
    private Boss_Attack bossAttack;

    [Header("Cours")]
    public float speed;

    [Header("Attaques")]
    public float timerSpecialAttack;
    public bool useSpecialAttack;
    public bool useAttack;

    [Header("Vie")]
    private float health = 9;

    [Header("Se retourne")]
    public bool isFlipped = false;


    private void Start()
    {
        objectBoss = objectGO.GetComponent<Object_Boss>();
        bossAttack = GetComponent<Boss_Attack>();
    }

    // Si le Joueur est mort, reset les timers et positions du Boss. Sinon, si le Boss est active, commence le combat.
    private void Update()
    {
        if (playerController.isDead)
        {
            isActivate = false;
            Reset();
        }

        if (isActivate)
        {
            timerSpecialAttack += Time.deltaTime;

            if (!useSpecialAttack && !useAttack)
            {
                if (playerRB.gravityScale == 60)
                {
                    Run();
                }

                if (playerRB.gravityScale == -60)
                {
                    bossAttack.Shooting();
                }
            }

            if (timerSpecialAttack >= 15)
            {
                useSpecialAttack = true;
                bossAttack.SpecialAttack();
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
        health -= 1;

        if (health <= 0)
        {
            Die();
            return;
        }

        Invincibility();
    }

    private void Invincibility()
    {
        isActivate = false;
        bossRB.isKinematic = true;
        boxCollider.enabled = false;
        hitbox.enabled = false; 
        StartCoroutine(InvincibilityTimer());
        //animation
    }

    private IEnumerator InvincibilityTimer()
    {
        yield return new WaitForSeconds(2);
        bossRB.isKinematic = false;
        boxCollider.enabled = true;
        hitbox.enabled = true;
        isActivate = true;
    }

    // Permet de tuer le Boss
    private void Die()
    {
        Debug.Log("Dead");
        Destroy(gameObject);
    }

    // Permet de reset le Boss
    private void Reset()
    {
        bossAwakeGO.SetActive(true);
        isActivate = false;
        timerSpecialAttack = 0;
        health = 9;
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
