using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkNoPowerUp : MonoBehaviour
{
    [Header("Autres")]
    public GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.GetComponent<Player_Controller>().getPowerUp = false;
            gameObject.SetActive(false);
        }
    }
}
