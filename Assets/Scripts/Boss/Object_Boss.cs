using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet d'avoir un objet qui change de gravité lorsque le Joueur appuie sur F et se détruit si contact avec quelque chose de mortel (ennemi, pieges...)
public class Object_Boss : MonoBehaviour
{
    [Header("Autres")]
    private Spawner_Object_Boss objectSpawner;
    private Boss_Brain bossBrain;
    public GameObject objectSpawnerGO; // "wallGO" signifie "wallGameObject"
    public GameObject boss;

    [Header("GameObject")]
    public Rigidbody2D objectRB;
    private Vector2 originalPosition;

    [Header("Inverser gravite")]
    private bool canChangeGravity = false;
    [SerializeField] private float originalGravity;

    [Header("Position")]
    [SerializeField] private float positionX;
    [SerializeField] private float positionY;

    [Header("Contact au sol")]
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        originalPosition = new Vector2(positionX, positionY);
        objectSpawner = objectSpawnerGO.GetComponent<Spawner_Object_Boss>();
        bossBrain = boss.GetComponent<Boss_Brain>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canChangeGravity && isGrounded())
        {
            changeGravity();
        }
    }

    // Permet de changer sa gravite si le Joueur interagit ou de se detruire si contact avec quelque chose de dangereux
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Permet au Joueur d'interagir
        {
            canChangeGravity = true;
            other.enabled = true;
        }

        if (other.CompareTag("Boss")) // Permet de faire des degats au Boss
        {
            Contact();
            objectSpawner.canDestroy = true;
            bossBrain.TakeDamage();
        }

        if (other.CompareTag("DeadlyProps")) // Permet de se detruire si autres contacts dangereux
        {
            Contact();
            objectSpawner.canDestroy = true;
        }
    }

    // Permet d'empecher le Joueur d'interagir si trop eloigne
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            canChangeGravity = false;
        }
    }

    // Permet de desactiver l'objet
    public void Contact()
    {
        gameObject.SetActive(false);
        gameObject.transform.position = originalPosition;
    }

    // Permet de changer la gravite de l'objet
    private void changeGravity()
    {
        originalGravity = -originalGravity;
        objectRB.gravityScale = originalGravity;
        Rotation();
    }

    // Permet de verifier si contact avec le sol
    public bool isGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }

    // Permet de retourner l'objet
    private void Rotation()
    {
        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
    }
}
