using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Permet de faire bouger l'objet selon des points donnés et de tuer le Joueur

public class Platform_Trap : MonoBehaviour
{
    [Header("GameObject")]
    private Rigidbody2D rb;

    [Header("Points")]
    private int pointIndex;
    private int pointCount;
    private Vector3 targetWaypoint;
    [SerializeField] private GameObject ways;
    [SerializeField] private Transform[] waypoints;

    [Header("Mouvements")]
    private Vector3 moveDirection;
    private int direction = 1;
    [SerializeField] private float speed;

    [Header("Timer")]
    [SerializeField] private float waitDuration;

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

    // Permet de définir le prochain point ciblé et de faire bouger l'objet dans la bonne direction en ayant des temps de pauses entre chaque point
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
        StartCoroutine(WaitNextPoint());
    }

    // Permet de faire attendre quelques secondes la plateforme avant d'aller a un prochain point
    private IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    // Permet de faire bouger l'objet de sa position à la celle du prochain point

    private void DirectionCalculate()
    {
        moveDirection = (targetWaypoint - transform.position).normalized;
    }

    // Permet à l'objet de tuer le Joueur
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Player_Controller player = other.transform.GetComponent<Player_Controller>();
            player.Die();
        }
    }
}
