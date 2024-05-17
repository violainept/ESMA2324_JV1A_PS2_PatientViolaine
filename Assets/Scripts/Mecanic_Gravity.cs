using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mecanic_Gravity : MonoBehaviour
{
    private Rigidbody2D rb;
    
    private bool top;

    [SerializeField] private Transform groundCheckLeft;
    [SerializeField] private Transform groundCheckRight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeGravity()
    {
        if (Input.GetKeyDown(KeyCode.G) && IsGrounded())
        {
            rb.gravityScale *= -1;
            Rotation();
        }
    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);
    }

    private void Rotation()
    {
        if (top == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180f);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }
        top = !top;
    }

}
