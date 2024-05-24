using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible_Achievement : MonoBehaviour
{
    public Collectible_Collected collectible;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (collectible.isCollected == true)
        {
            anim.SetTrigger("isCollected");
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
    
}
