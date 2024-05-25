using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet au Joueur de mourir
public class Player_Death : MonoBehaviour
{
    public GameObject player;

    private Transform playerSpawn;
    private Animator fadeSystem;
    public bool isDead = false;

    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
        fadeSystem = GameObject.FindGameObjectWithTag("FadeSystem").GetComponent<Animator>();
    }
    private void Update()
    {
        if (isDead)
        {
            StartCoroutine(Death());
        }
    }

    private IEnumerator Death()
    {
        isDead = false;
        fadeSystem.SetTrigger("FadeIn");
        yield return new WaitForSeconds(2);
        player.transform.position = playerSpawn.position; 
    }
}
