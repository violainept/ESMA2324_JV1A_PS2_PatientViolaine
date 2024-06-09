using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet d'activer le fonctionnement de la plateforme

public class Platform_Disappear : MonoBehaviour
{
    [Header("Autre")]
    private Platform_Disappear_Activated_Desactivated platform;
    public GameObject platformGO;

    [Header("GameObject")]
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        platform = platformGO.GetComponent<Platform_Disappear_Activated_Desactivated>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            anim.SetBool("startProcess", true);
        }
    }

    public void CallPlatformSystem()
    {
        platform.CallPlatformSystem();
    }
}
