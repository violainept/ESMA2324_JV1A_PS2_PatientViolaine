using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject platform;
    public Animator anim;

    private float shakingTime = 1f;
    private float disappearingTime = 1f;

    public void CallPlatformSystem()
    {
        StartCoroutine(PlatformSystem());
    }
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
