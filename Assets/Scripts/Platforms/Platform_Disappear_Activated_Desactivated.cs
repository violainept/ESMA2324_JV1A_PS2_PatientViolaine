using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire disparaitre la plateforme au bout d'un certain temps puis de la faire reapparaitre ensuite

public class Platform_Disappear_Activated_Desactivated : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject platform;
    private Platform_Disappear platformProcess;

    [Header("Timers")]
    private float disappearingTime = 1f;

    private void Start()
    {
        platformProcess = platform.GetComponent<Platform_Disappear>();
    }
    public void CallPlatformSystem()
    {
        StartCoroutine(PlatformSystem());
    }

    // Permet de faire disparaitre la plateforme, d'attendre puis de la refaire apparaitre
    private IEnumerator PlatformSystem()
    {
        platform.SetActive(false);
        yield return new WaitForSeconds(disappearingTime);
        platform.SetActive(true);
        platformProcess.anim.SetBool("startProcess", false);
    }
}
