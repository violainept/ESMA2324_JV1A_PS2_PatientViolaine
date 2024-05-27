using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet d'avoir un objet qui change de gravité lorsque le Joueur appuie sur F et se détruit si contact avec quelque chose de mortel (ennemi, pieges...)
public class Object_UsingGravity : MonoBehaviour
{
    [Header("Autres")]
    public Object_Spawner objectSpawner;
    public GameObject spawner;

    [Header("GameObject")]
    public Rigidbody2D rb;
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
        objectSpawner = spawner.GetComponent<Object_Spawner>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && canChangeGravity && isGrounded())
        {
            changeGravity();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            canChangeGravity = true;
        }

        if (other.transform.CompareTag("Enemy") || other.transform.CompareTag("DeadlyProps"))
        {
            objectSpawner.Respawn();
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
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
