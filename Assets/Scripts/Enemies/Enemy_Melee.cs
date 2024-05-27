using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet que l'ennemi patrouille puis attaque le Joueur lorsqu'il rentre dans son champ de vision

public class Enemy_behaviour : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //
    
    public Player_Controller playerHealth;
    public GameObject player;

    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform rayCast;
    public Transform leftLimit;
    public Transform rightLimit;
    public LayerMask raycastMask;

    private float distance;
    private float intTimer;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private Transform target;
    private Animator anim;
    private RaycastHit2D hit;

    private void Awake()
    {

        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        playerHealth = player.GetComponent<Player_Controller>();
    }

    private void Update()
    {
        // Si l'ennemi n'est pas en train d'attaquer, il patrouille
        if (!attackMode)
        {
            Move();
        }

        if (!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            SelectTarget();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        // Si le Joueur est detecte
        if (hit.collider != null)
        {
            EnemyLogic();
        }
        else if (hit.collider == null)
        {
            inRange = false;
        }

        // Si le Joueur n'est plus visible
        if (inRange == false)
        {
            StopAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    // ----------------------------------------------------------------------------------- Logique de l'ennemi ----------------------------------------------------------------------------------- //
    // Verifie sa position par rapport a celle du Joueur. Si le Joueur est pres, l'Ennemi attaque. Au contraire, il s'arrête d'attaquer si le Joueur est eloigne. 
    private void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDistance)
        {
            StopAttack();
        }
        else if (attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    // ----------------------------------------------------------------------------------- Deplacements ----------------------------------------------------------------------------------- //
    // Si l'ennemi n'attaque pas, il se deplace
    private void Move()
    {
        anim.SetBool("canWalk", true);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    // ----------------------------------------------------------------------------------- Attaque ----------------------------------------------------------------------------------- //
    // Lorsque le Joueur est proche de l'ennemi, l'ennemi attaque
    private void Attack()
    {
        timer = intTimer;
        attackMode = true;
        playerHealth.isDead = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    // ----------------------------------------------------------------------------------- Timer ----------------------------------------------------------------------------------- //
    // Attend quelques instants de verifier si le Joueur est visible ou non
    private void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    // ----------------------------------------------------------------------------------- Arrêt Attaque ----------------------------------------------------------------------------------- //
    // Si le Joueur n'est plus visible, il s'arrete
    private void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
    }

    // ----------------------------------------------------------------------------------- RayCast ----------------------------------------------------------------------------------- //
    // Permet de faire apparaitre une ligne qui change de couleur selon si le Joueur est proche de l'ennemi ou non

    private void RaycastDebugger()
    {
        if (distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    // ----------------------------------------------------------------------------------- Trigger ----------------------------------------------------------------------------------- //
    public void TriggerCooling()
    {
        cooling = true;
    }

    // ----------------------------------------------------------------------------------- Limites ----------------------------------------------------------------------------------- //
    // Definit les limites de la patrouille

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    // ----------------------------------------------------------------------------------- Choix cible ----------------------------------------------------------------------------------- //
    // Permet de definir la cible de l'ennemi
    private void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    // ----------------------------------------------------------------------------------- Visuel/GameObject : Se retourne ----------------------------------------------------------------------------------- //
    // Se retourne si lorsqu'il doit tourner dans le sens inverse

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180;
        }
        else
        {
            rotation.y = 0;
        }

        transform.eulerAngles = rotation;
    }
}
