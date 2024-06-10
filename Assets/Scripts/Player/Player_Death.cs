using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Death : MonoBehaviour
{
    [Header("Autre")]
    private Transform playerSpawn;
    public Transform playerTR;

    public Player_Controller player;
    public Animator animPlayer;

    public bool isDead;

    private Vector2 originalScale;


    private void Awake()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
    }

    private void Start()
    {
        originalScale = new Vector2(0.5675f, 0.5675f);
    }

    private void Update()
    {
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawn").transform;

        if (isDead)
        {
            if (player.rb.gravityScale == 20 || player.rb.gravityScale == 60)
            {
                StartCoroutine(isDyingDown());
                isDead = false;
            }

            if (player.rb.gravityScale == -20 || player.rb.gravityScale == -60)
            {
                StartCoroutine(isDyingUp());
                isDead = false;
            }

        }
    }

    private IEnumerator isDyingDown()
    {
        animPlayer.SetTrigger("isDyingDown");
        yield return new WaitForSeconds(0.2f);
        player.transform.position = playerSpawn.position;
        player.rb.velocity = Vector3.zero;
        animPlayer.SetTrigger("Respawn");
    }

    private IEnumerator isDyingUp()
    {
        animPlayer.SetTrigger("isDyingUp");
        yield return new WaitForSeconds(0.2f);
        playerTR.position = playerSpawn.position;
        player.rb.velocity = Vector3.zero;
        animPlayer.SetBool("usingGravity", false);
        animPlayer.SetTrigger("Respawn");
        player.Rotation();
        player.rb.gravityScale = 60;

    }
}
