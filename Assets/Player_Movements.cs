using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movements : MonoBehaviour
{
    [Header("Configurations")]

    private float horizontal;
    private float speed = 5f;

    private bool isJumping = false;

    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;
    [SerializeField] private LayerMask groundLayer;



    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        MovePlayer();
        JumpPlayer();

        Debug.Log(IsGrounded());
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private void JumpPlayer()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            isJumping = false;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }
}
