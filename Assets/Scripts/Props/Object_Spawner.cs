using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire apparaitre l'objet au debut et si detruit

public class Object_Spawner : MonoBehaviour
{
    [Header("GameObject")]
    public GameObject prefab;

    [Header("Position")]
    public Transform spawnPoint;

    private void Start()
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        prefab = GameObject.FindGameObjectWithTag("Object");
    }

    // Fait apparaitre de nouveau l'objet
    public void Respawn()
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
