using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Awake : MonoBehaviour
{
    private Boss_Brain enemyBoss;
    public GameObject enemy;

    private void Start()
    {
        enemyBoss = enemy.GetComponent<Boss_Brain>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            enemyBoss.isActivate = true;
            gameObject.SetActive(false);
        }
    }
}
