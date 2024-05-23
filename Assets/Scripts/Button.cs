using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject obstacle;
    public BoxCollider2D button;
    public Animator anim;

    private Vector2 originalSize;

    private void Start()
    {
        button = GetComponent<BoxCollider2D>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("isPushed", true);
            originalSize = button.size;
            button.size = new Vector3(1.071056f, 0.5422902f, 0);
            obstacle.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("isPushed", false);
            button.size = originalSize;
            obstacle.SetActive(true);
        }

    }
}
