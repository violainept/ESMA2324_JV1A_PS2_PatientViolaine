using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject obstacle;
    public Animator obstacleAnim;

    public BoxCollider2D buttonCollider;
    public Animator buttonAnim;

    private Vector2 originalSize;
    private Vector2 originalOffset;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Objects"))
        {
            buttonAnim.SetBool("isPushed", true);
            originalSize = buttonCollider.size;
            originalOffset = buttonCollider.offset;
            buttonCollider.size = new Vector3(1.111111f, 0, 0);
            buttonCollider.offset = new Vector3(0, -0.5555556f, 0);
            StartCoroutine(obstacleDesactivated());
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Objects"))
        {
            buttonAnim.SetBool("isPushed", false);
            buttonCollider.size = originalSize;
            buttonCollider.offset = originalOffset;
            StartCoroutine(obstacleActivated());
        }
    }

    private IEnumerator obstacleActivated()
    {
        obstacleAnim.SetBool("isVisible", true);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator obstacleDesactivated()
    {
        obstacleAnim.SetBool("isVisible", false);
        yield return new WaitForSeconds(1);
    }
}
