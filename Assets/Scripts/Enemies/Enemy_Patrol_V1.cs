using UnityEngine;
using UnityEngine.UIElements;

public class Enemy_Patrol_V1 : MonoBehaviour
{
    private int destinationPoint = 0;

    public float speed;

    public Transform[] waypoints;
    private Transform target;
    public SpriteRenderer graphics;

    void Start()
    {
        target = waypoints[0];
    }

    void Update()
    {
        Patrol();
    }

    private void Patrol()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destinationPoint = (destinationPoint + 1) % waypoints.Length;
            target = waypoints[destinationPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            Player_Controller playerController = collision.transform.GetComponent<Player_Controller>();
            playerController.Die();
        }
    }
}