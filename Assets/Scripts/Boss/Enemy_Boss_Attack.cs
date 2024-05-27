using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Enemy_Boss_Attack : MonoBehaviour
{
    [Header("Autres")]
    public Player_Controller playerController;
    public GameObject deadZone1;
    public GameObject deadZone2;

    [Header("Lien Script")]
    private Enemy_Boss_Brain enemyBrain;

    [Header("GameObject")]
    public Rigidbody2D enemyRB;
    private Vector2 originalPosition;

    [Header("Attaque")]
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    [Header("Attaque Speciale")]
    private float dashingPower = 50f;
    private float dashingTime = 0.2f;
    [SerializeField] private TrailRenderer tr;

    // Permet au Boss d'attaquer
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        StartCoroutine(Dash());

        if (colInfo != null)
        {
            Debug.Log("Damage player");
            enemyBrain.timerCooldown = 6;
        }
    }

    // Permet au Boss de faire un Dash (attaque)
    private IEnumerator Dash()
    {
        Debug.Log("Actif");
        enemyRB.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
    }

    // Permet au Boss de changer la gravite du Joueur (attaque speciale)
    public void SpecialAttack()
    {
        playerController.ChangeGravity();
        enemyBrain.timerSpecialAttack = 0;

        if (enemyRB.gravityScale == -10)
        {
            StartCoroutine(DeathZoneDown());
        }

        if (enemyRB.gravityScale == 10)
        {
            StartCoroutine(DeathZoneUp());
        }
    }

    private IEnumerator DeathZoneDown()
    {
        deadZone1.SetActive(true);
        yield return new WaitForSeconds(5);
        deadZone1.SetActive(false);
    }

    private IEnumerator DeathZoneUp()
    {
        deadZone2.SetActive(true);
        yield return new WaitForSeconds(5);
        deadZone2.SetActive(false);
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
