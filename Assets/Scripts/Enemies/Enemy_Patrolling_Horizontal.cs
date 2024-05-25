using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Script permettant de faire bouger l'ennemi selon des points donnés et de tuer le Joueur
public class Enemy_Patrolling_Horizontal : MonoBehaviour
{

    // ----------------------------------------------------------------------------------- Proprietes et Variables ----------------------------------------------------------------------------------- //


    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 targetWaypoint;

    [SerializeField] private float speed;

    [SerializeField] private GameObject ways;
    [SerializeField] private Transform[] waypoints;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        waypoints = new Transform[ways.transform.childCount];

        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            waypoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    private void Start()
    {
        pointIndex = 1;
        pointCount = waypoints.Length;
        targetWaypoint = waypoints[1].transform.position;
        DirectionCalculate();
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, targetWaypoint) < 0.05f)
        {
            NextPoint();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDirection * speed;
    }


    // Si l'ennemi se rapproche du dernier point, alors il fait demi-tour. Inversement si il arrive au point de départ.
    // Permet de définir le prochain point ciblé et de faire bouger l'ennemi dans la bonne direction
    private void NextPoint()
    {
        transform.position = targetWaypoint;
        moveDirection = Vector3.zero;

        if (pointIndex == pointCount - 1)
        {
            direction = -1;
        }

        if (pointIndex == 0)
        {
            direction = 1;
        }

        pointIndex += direction;
        targetWaypoint = waypoints[pointIndex].transform.position;
        DirectionCalculate();
    }


    // Calcul de la distance entre le prochain point et le point actuel
    // Permet de faire bouger l'ennemi de sa position à la celle du prochain point

    private void DirectionCalculate()
    {
        moveDirection = (targetWaypoint - transform.position).normalized;
    }

    // Si le Joueur rentre en contact avec l'ennemi, l'ennemi appelle la fonction Tuer
    // Permet à l'ennemi de tuer le Joueur
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player_Death playerDeath = other.transform.GetComponent<Player_Death>();
            playerDeath.isDead = true;
        }
    }
}
