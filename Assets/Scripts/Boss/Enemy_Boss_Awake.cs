using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss_Awake : MonoBehaviour
{
    public Enemy_Boss_Brain enemyBoss;
    public GameObject enemy;

    private void Start()
    {
        enemyBoss = enemy.GetComponent<Enemy_Boss_Brain>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyBoss.isActivate = true;
        }
    }
}
