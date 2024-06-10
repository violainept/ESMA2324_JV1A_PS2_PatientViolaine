using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Permet de faire bouger l'ennemi selon des points donnés et de tuer le Joueur

public class Enemy_Patrolling_Horizontal : MonoBehaviour
{
    [Header("Points")]
    private int pointIndex;
    private int pointCount;
    [SerializeField] private GameObject ways;
    [SerializeField] private Transform[] waypoints;

    [Header("Mouvements")]
    private Vector3 moveDirection;
    private Vector3 targetWaypoint;
    private int direction = 1;
    [SerializeField] private float speed;

    [Header("GameObject")]
    private Rigidbody2D rb;
    public SpriteRenderer sprite;

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


    // Permet de définir le prochain point ciblé et de faire bouger l'ennemi dans la bonne direction
    private void NextPoint()
    {
        transform.position = targetWaypoint;
        moveDirection = Vector3.zero;

        if (pointIndex == pointCount - 1)
        {
            direction = -1;
            sprite.flipX = true;
        }

        if (pointIndex == 0)
        {
            direction = 1;
            sprite.flipX = false;
        }

        pointIndex += direction;
        targetWaypoint = waypoints[pointIndex].transform.position;
        DirectionCalculate();
    }


    // Permet de faire bouger l'ennemi de sa position à la celle du prochain point

    private void DirectionCalculate()
    {
        moveDirection = (targetWaypoint - transform.position).normalized;
    }

    // Permet à l'ennemi de tuer le Joueur
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player_Death player = other.gameObject.GetComponent<Player_Death>();
            player.isDead = true;
        }
    }
}
