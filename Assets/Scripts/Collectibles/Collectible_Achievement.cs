using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire apparaitre un trophee une fois le collectible recupere

public class Collectible_Achievement : MonoBehaviour
{
    [Header("Autre")]
    private Collectible_Interaction collectibleCollected;
    public GameObject collectible;

    [Header("GameObject")]
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        collectibleCollected = collectible.GetComponent<Collectible_Interaction>();
    }

    private void Update()
    {
        if (collectibleCollected.isCollected == true)
        {
            anim.SetTrigger("isCollected");
        }
    }

    // Permet de detruire le panel collectible une fois recupere
    private void Destroy()
    {
        Destroy(gameObject);
    }
    
}
