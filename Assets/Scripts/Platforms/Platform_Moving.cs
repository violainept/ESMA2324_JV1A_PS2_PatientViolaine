using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Script permettant de faire bouger la plateforme selon des points donnés
public class Platform_Moving : MonoBehaviour
{

    // ----------------------------------------------------------------------------------- Propriétés et Variables ----------------------------------------------------------------------------------- //


    private int pointIndex;
    private int pointCount;
    private int direction = 1;

    private Rigidbody2D rb;
    private Rigidbody2D playerRb;
    private Vector3 moveDirection;
    private Vector3 targetWaypoint;

    [SerializeField] private float speed;
    [SerializeField] private float waitDuration;

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

    // Définition des variables et propriétés de la plateforme
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

    // Si la plateforme se rapproche du dernier point, alors elle fait demi-tour. Inversement si elle arrive au point de départ.
    // Permet de définir le prochain point ciblé et de faire bouger la plateforme dans la bonne direction en ayant des temps de pauses entre chaque point
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
    // Permet de faire bouger la plateforme de sa position à la celle du prochain point

    private void DirectionCalculate()
    {
        moveDirection = (targetWaypoint - transform.position).normalized;
    }

    // ----------------------------------------------------------------------------------- Bouger le Joueur avec la plateforme  ----------------------------------------------------------------------------------- //

    // Si le Joueur rentre en contact avec la plateforme, il devient enfant du GameObject. A l'inverse, s'il sort de son champ de contact, il n'est plus enfant
    // Permet de faire bouger le Joueur avec la plateforme
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTrigerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
        }
    }
}
