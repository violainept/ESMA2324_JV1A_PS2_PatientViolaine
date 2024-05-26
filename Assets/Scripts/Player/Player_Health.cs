using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet au Joueur de mourir
public class Player_Health : MonoBehaviour
{
    public GameObject player;
    private Animator fadeSystem;
    public bool isDead = false;

    private void Awake()
    {
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDead)
        {
            StartCoroutine(Death());
        }
    }

    private void Health()
    {

    }

    public void TakeDamage()
    {

    }
    private IEnumerator Death()
    {
        isDead = false;
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
    }
}
