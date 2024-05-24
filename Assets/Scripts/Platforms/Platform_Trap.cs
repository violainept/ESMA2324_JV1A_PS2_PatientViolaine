using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Script permettant de faire bouger l'objet selon des points donnés et de tuer le Joueur
public class Platform_Trap : MonoBehaviour
{

    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //


    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    private Rigidbody2D rb;
    private Vector3 moveDirection;
    private Vector3 targetWaypoint;

    [SerializeField] private float speed;
    [SerializeField] private float waitDuration;

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

    // Définition des variables et propriétés de l'objet
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

    // ----------------------------------------------------------------------------------- Prochain point ----------------------------------------------------------------------------------- //

    // Si l'objet se rapproche du dernier point, alors il fait demi-tour. Inversement si il arrive au point de départ.
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

    // ----------------------------------------------------------------------------------- Attente entre les points ----------------------------------------------------------------------------------- //

    // Attends le nombre de secondes defini par la variable puis lance la fonction de calcul de direction
    // Permet de faire attendre quelques secondes la plateforme avant d'aller a un prochain point
    private IEnumerator WaitNextPoint()
    {
        yield return new WaitForSeconds(waitDuration);
        DirectionCalculate();
    }

    // ----------------------------------------------------------------------------------- Calcul de la direction ----------------------------------------------------------------------------------- //

    // Calcul de la distance entre le prochain point et le point actuel
    // Permet de faire bouger l'objet de sa position à la celle du prochain point

    private void DirectionCalculate()
    {
        moveDirection = (targetWaypoint - transform.position).normalized;
    }

    // ----------------------------------------------------------------------------------- Tuer le Joueur  ----------------------------------------------------------------------------------- //

    // Si le Joueur rentre en contact avec l'objet, l'objet appelle la fonction Tuer
    // Permet à l'objet de tuer le Joueur
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player_Controller playerController = collision.transform.GetComponent<Player_Controller>();
            playerController.isDead = true;
        }
    }
}
