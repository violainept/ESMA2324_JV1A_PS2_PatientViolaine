using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet d'activer le fonctionnement de la plateforme

public class Platform_Disappear : MonoBehaviour
{
    [Header("Autre")]
    public Platform_Disappear_Activated_Desactivated platform;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            platform.CallPlatformSystem();
        }
    }
}
