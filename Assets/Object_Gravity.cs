using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Gravity : MonoBehaviour
{

    private bool canChangeGravity = false;

    public Rigidbody2D rb;
    private GameObject goObject; // 'goObject' pour 'gameobjectObject'
    private Vector2 originalPosition;

    [SerializeField] private float positionX;
    [SerializeField] private float positionY;
    [SerializeField] private float originalGravity;

    public bool isDestroy = false;

    // Contact avec le sol
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        originalPosition = new Vector2(positionX, positionY);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canChangeGravity && isGrounded())
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

        if (other.transform.CompareTag("Enemy"))
        {
            isDestroy = true;
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
