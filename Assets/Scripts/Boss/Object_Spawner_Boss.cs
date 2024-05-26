using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet de faire apparaitre l'objet au debut et si detruit
public class Object_Spawner_Boss : MonoBehaviour
{
    public GameObject prefab;
    public Transform spawnPoint;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Spawn();
        }
    }

    // Fait apparaitre de nouveau l'objet
    public void Spawn()
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
