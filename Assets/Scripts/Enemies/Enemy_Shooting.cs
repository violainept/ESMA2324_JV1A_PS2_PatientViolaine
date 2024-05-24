using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet que l'ennemi tire en direction du Joueur
public class Enemy_Shooting : MonoBehaviour
{
    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //
    public Player_Controller playerController;

    private GameObject player;

    public GameObject projectile;
    public Transform projectilePos;

    private float timer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Permet a l'ennemi de tirer en direction du Joueur
    private void Update()
    {
        if (playerController.isDead == false)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < 10)
            {
                timer += Time.deltaTime;

                if (timer > 2)
                {
                    timer = 0;
                    Shoot();
                }
            }
        }
    }

    // Permet a l'ennemi de faire apparaitre un projectile
    private void Shoot()
    {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }
}
