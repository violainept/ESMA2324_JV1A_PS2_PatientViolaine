using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
    public GameObject projectile;
    public Transform projectilePos;
    private float timer;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        timer += Time.deltaTime;

        //float distance = Vector2.Distance(transform.position, )

        if (timer > 2)
        {
            timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {

    }
}
