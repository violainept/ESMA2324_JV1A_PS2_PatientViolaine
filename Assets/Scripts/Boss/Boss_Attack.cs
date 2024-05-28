using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Boss_Attack : MonoBehaviour
{
    [Header("Autres")]
    public Player_Controller playerController;
    public Transform player;
    public Rigidbody2D playerRB; // "playerRB" signifie "playerRigidBody"

    [Header("Lien Script")]
    private Boss_Brain bossBrain;

    [Header("GameObject")]
    public Rigidbody2D bossRB; // "bossRB" signifie "bossRigidBody"

    [Header("Attaque Sol")]
    public float attackRange;
    public Vector3 attackOffset;
    public LayerMask attackMask;
    private float dashingPower = 26f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    public TrailRenderer tr;

    [Header("Attaque Plafond")]
    public GameObject projectile;
    private float timerShooting;

    [Header("Attaque Speciale")]
    public GameObject deathZoneUp;
    public GameObject deathZoneDown;
    private bool useDeathZone;
    private bool deathZone1AlreadyActivated;
    private bool deathZone2AlreadyActivated;
    private float timerDeathZone;

    private void Start()
    {
        bossBrain = GetComponent<Boss_Brain>();
    }
    // Permet au Boss de tirer (si Joueur au plafond)
    public void Shooting()
    {
        timerShooting += Time.deltaTime;

        if (timerShooting > 3)
        {
            timerShooting = 0;
            Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        }
    }

    // Permet au Boss d'attaquer (si Joueur au sol)
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (colInfo != null)
        {
            bossBrain.useAttack = true;

            if (transform.position.x > player.position.x)
            {
                StartCoroutine(DashRight());
            }

            if (transform.position.x < player.position.x)
            {
                StartCoroutine(DashLeft());
            }
        }
    }

    // Permet au Boss de faire un Dash (attaque)
    private IEnumerator DashRight()
    {
        yield return new WaitForSeconds(1);

        float originalGravity = bossRB.gravityScale;
        bossRB.gravityScale = 0f;
        bossRB.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        bossRB.gravityScale = originalGravity;
        bossRB.velocity = new Vector2(transform.localScale.x / dashingPower, 0f);

        yield return new WaitForSeconds(dashingCooldown);

        bossBrain.useAttack = false;
    }

    private IEnumerator DashLeft()
    {
        yield return new WaitForSeconds(1);

        float originalGravity = bossRB.gravityScale;
        bossRB.gravityScale = 0f;
        bossRB.velocity = new Vector2(transform.localScale.x * -dashingPower, 0f);
        tr.emitting = true;

        yield return new WaitForSeconds(dashingTime);

        tr.emitting = false;
        bossRB.gravityScale = originalGravity;
        bossRB.velocity = new Vector2((transform.localScale.x * (-1)) / dashingPower, 0f);

        yield return new WaitForSeconds(dashingCooldown);

        bossBrain.useAttack = false;
    }

    // Permet au Boss de changer la gravite du Joueur (attaque speciale)
    public void SpecialAttack()
    {
        bossBrain.useSpecialAttack = true;

        if (!useDeathZone)
        {
            playerController.ChangeGravity();
        }

        useDeathZone = true;

        if (playerRB.gravityScale == -60 && !deathZone2AlreadyActivated)
        {
            deathZone1AlreadyActivated = true;
            timerDeathZone += Time.deltaTime;
            deathZoneUp.SetActive(true);

            if (timerDeathZone > 5)
            {
                deathZoneUp.SetActive(false);
                bossBrain.useSpecialAttack = false;
                bossBrain.timerSpecialAttack = 0;
                timerDeathZone = 0;
                deathZone1AlreadyActivated = false;
                useDeathZone = false;
            }
        }

        if (playerRB.gravityScale == 60 && !deathZone1AlreadyActivated)
        {
            deathZone2AlreadyActivated = true;
            timerDeathZone += Time.deltaTime;
            deathZoneDown.SetActive(true);

            if (timerDeathZone > 5)
            {
                deathZoneDown.SetActive(false);
                bossBrain.useSpecialAttack = false;
                bossBrain.timerSpecialAttack = 0;
                timerDeathZone = 0;
                deathZone2AlreadyActivated = false;
                useDeathZone = false;
            }
        }
    }

    // Permet au Joueur de prendre des degats si contact avec le Boss
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.isDead = true;
        }
    }

    // Permet de dessiner un gizmo
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}
