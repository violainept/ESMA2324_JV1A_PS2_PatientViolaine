using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire bouger une plateforme

public class Platform_Moving : MonoBehaviour
{
    [Header("Autre")]
    public Rigidbody2D playerRB;
    private Player_Controller playerController;
    public GameObject player;

    [Header("GameObject")]
    public Rigidbody2D platformRB;

    [Header("Points")]
    public Transform posA, posB;
    Vector3 targetPos;

    [Header("Vitesse")]
    public float speed;


    private void Start()
    {
        targetPos = posB.position;
        playerController = player.GetComponent<Player_Controller>();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < 0.05f)
        {
            targetPos = posB.position;
        }

        if (Vector2.Distance(transform.position, posB.position) < 0.05f)
        {
            targetPos = posA.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }
}
