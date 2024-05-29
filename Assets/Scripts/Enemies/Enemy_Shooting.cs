using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet a l'ennemi de tirer en direction du Joueur

public class Enemy_Shooting : MonoBehaviour
{
    [Header("Autres")]
    public Player_Controller playerController;
    private GameObject player;

    [Header("GameObject")]
    public GameObject projectile;
    public Transform projectilePos;

    [Header("Timer")]
    private float timer;

    [Header("Distance")]
    public float playerDistance;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Permet a l'ennemi de tirer en direction du Joueur
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < playerDistance)
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                Shoot();
            }
        }
    }

    // Permet a l'ennemi de faire apparaitre un projectile
    private void Shoot()
    {
        Instantiate(projectile, projectilePos.position, Quaternion.identity);
    }
}
