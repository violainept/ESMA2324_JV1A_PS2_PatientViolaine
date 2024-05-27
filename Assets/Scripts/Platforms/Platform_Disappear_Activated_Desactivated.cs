using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire disparaitre la plateforme au bout d'un certain temps puis de la faire reapparaitre ensuite

public class Platform_Disappear_Activated_Desactivated : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject platform;
    public Animator anim;

    [Header("Timers")]
    private float shakingTime = 1f;
    private float disappearingTime = 1f;

    public void CallPlatformSystem()
    {
        StartCoroutine(PlatformSystem());
    }

    // Permet de faire disparaitre la plateforme, d'attendre puis de la refaire apparaitre
    private IEnumerator PlatformSystem()
    {
        anim.SetBool("isDisapearring", true);
        yield return new WaitForSeconds(shakingTime);
        platform.SetActive(false);
        Debug.Log("I'm desactivated");
        yield return new WaitForSeconds(disappearingTime);
        Debug.Log("I'm activated");
        platform.SetActive(true);
    }
}
