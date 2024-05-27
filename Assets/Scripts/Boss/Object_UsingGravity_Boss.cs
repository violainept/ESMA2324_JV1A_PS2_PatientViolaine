using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet d'avoir un objet qui change de gravit� lorsque le Joueur appuie sur F et se d�truit si contact avec quelque chose de mortel (ennemi, pieges...)
public class Object_UsingGravity_Boss : MonoBehaviour
{
    // Autres GameObject/script
    public Object_Spawner_Boss wall;
    public GameObject wallGO; // "wallGO" signifie "wallGameObject"

    // GameObject
    public GameObject objectGO;
    public Rigidbody2D objectRB;
    private Vector2 originalPosition;

    // Gravite
    private bool canChangeGravity = false;
    [SerializeField] private float originalGravity;

    // Cordonnees
    [SerializeField] private float positionX;
    [SerializeField] private float positionY;

    // Contact avec le sol
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        originalPosition = new Vector2(positionX, positionY);
        wall = wallGO.GetComponent<Object_Spawner_Boss>();
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
        if (other.CompareTag("Player"))
        {
            canChangeGravity = true;
        }

        if (other.CompareTag("DeadlyProps"))
        {
            Contact();
            wall.canDestroy = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            canChangeGravity = false;
        }
    }

    public void Contact()
    {
        objectGO.SetActive(false);
        objectGO.transform.position = originalPosition;
    }
    private void changeGravity()
    {
        originalGravity = -originalGravity;
        objectRB.gravityScale = originalGravity;
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
