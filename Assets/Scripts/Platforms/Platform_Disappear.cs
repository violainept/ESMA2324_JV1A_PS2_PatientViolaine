using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Disappear : MonoBehaviour
{
    public Platform_Disappear_Activated_Desactivated platform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            platform.CallPlatformSystem();
        }
    }
}
