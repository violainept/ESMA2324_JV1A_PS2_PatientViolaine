using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievement_Unlocked : MonoBehaviour
{
    public Collectible_Collection collectible;
    public Animator anim;

    private void Start()
    {
        collectible = GameObject.FindObjectOfType(typeof(Collectible_Collection)) as Collectible_Collection;
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
