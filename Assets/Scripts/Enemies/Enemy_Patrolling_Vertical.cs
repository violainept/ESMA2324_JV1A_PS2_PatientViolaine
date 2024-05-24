using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Script permettant de faire bouger la plateforme selon des points donn�s
public class Enemy_Patrolling_Vertical : MonoBehaviour
{
    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    private Vector3 moveDirection;
    private Vector3 targetWaypoint;

    [SerializeField] private float speed;
    [SerializeField] private float waitDuration;
    [SerializeField] private float waitJump;
    [SerializeField] private float jump;

    [SerializeField] private GameObject ways;
    [SerializeField] private Transform[] waypoints;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        waypoints = new Transform[ways.transform.childCount];

        for (int i = 0; i < ways.gameObject.transform.childCount; i++)
        {
            waypoints[i] = ways.transform.GetChild(i).gameObject.transform;
        }
    }

    // D�finition des variables et propri�t�s de la plateforme
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

    // Si la plateforme se rapproche du dernier point, alors elle fait demi-tour. Inversement si elle arrive au point de d�part.
    // Permet de d�finir le prochain point cibl� et de faire bouger la plateforme dans la bonne direction en ayant des temps de pauses entre chaque point
    private void NextPoint()
    {
        transform.position = targetWaypoint;
        moveDirection = Vector3.zero;

        if (pointIndex == pointCount - 1)
        {
            direction = -1;
            pointIndex += direction;
            targetWaypoint = waypoints[pointIndex].transform.position;
            StartCoroutine(WaitNextPoint());
            return;
        }

        if (pointIndex == 0)
        {
            direction = 1;
            pointIndex += direction;
            targetWaypoint = waypoints[pointIndex].transform.position;
            StartCoroutine(WaitNextPointV2());
            return;
        }
    }

    // Attends le nombre de secondes defini par la variable puis lance la fonction de calcul de direction
    // Permet de faire attendre quelques secondes la plateforme avant d'aller a un prochain point
    private IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    private IEnumerator WaitNextPointV2()
    {
        yield return new WaitForSeconds(waitJump);
        rb.AddForce(new Vector3(rb.velocity.x, jump));
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    // Calcul de la distance entre le prochain point et le point actuel
    // Permet de faire bouger la plateforme de sa position � la celle du prochain point

    private void DirectionCalculate()
    {
        moveDirection = (targetWaypoint - transform.position).normalized;
    }
}