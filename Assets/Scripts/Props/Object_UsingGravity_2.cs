using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet d'avoir un objet qui change de gravite lorsque le Joueur appuie sur F et se détruit si contact avec quelque chose de mortel (ennemi, pieges...)

public class Object_UsingGravity_2 : MonoBehaviour
{
    [Header("Autres")]
    public Object_Spawner objectSpawner;
    private GameObject spawner;

    [Header("GameObject")]
    public Rigidbody2D rb;
    private Animator anim;

    [Header("Inverser gravite")]
    private bool canChangeGravity = false;
    [SerializeField] private float originalGravity;

    [Header("Respawn")]
    private bool canBeDestroy = false;


    [Header("Contact au sol")]
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        spawner = GameObject.FindGameObjectWithTag("Spawner2");
        objectSpawner = spawner.GetComponent<Object_Spawner>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canChangeGravity && isGrounded())
        {
            changeGravity();
        }
        if (Input.GetKeyDown(KeyCode.R) && canBeDestroy)
        {
            isDestroyed();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            anim.SetBool("Awake", true);
            canChangeGravity = true;
            canBeDestroy = true;
        }

        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("DeadlyProps"))
        {
            isDestroyed();
        }
    }

    private void isDestroyed()
    {
        objectSpawner.CoroutineStart();
        Destroy(gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            anim.SetBool("Awake", false);
            canChangeGravity = false;
        }
    }

    private void changeGravity()
    {
        originalGravity = -originalGravity;
        rb.gravityScale = originalGravity;
        Rotation();
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }
    private void Rotation()
    {
        Vector3 ScalerUP = transform.localScale;
        ScalerUP.y *= -1;
        transform.localScale = ScalerUP;
    }
}
