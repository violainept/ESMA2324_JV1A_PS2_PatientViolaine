using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Disappear : MonoBehaviour
{
    public Test test;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            test.CallPlatformSystem();
        }
    }
}
