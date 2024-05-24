using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test_ : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool top;
    public float jumpForce;
    public Vector2 originalVelocity;

    public void Start()
    {
        originalVelocity = rb.velocity;
    }
    public void Update()
    {
        if (top)
        {
            StartCoroutine(gravityDesactivated());
        }

        if (!top)
        {
            StartCoroutine(gravityActivated());
        }
    }

    public IEnumerator gravityDesactivated()
    {
        rb.gravityScale = -10;
        yield return new WaitForSeconds(2);
        top = false;
    }

    public IEnumerator gravityActivated()
    {
        rb.gravityScale = 10;
        yield return new WaitForSeconds(2);
        top = true;
    }
}
