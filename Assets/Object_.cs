using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_ : MonoBehaviour
{
    public Object_Gravity objectGravity;

    public Transform spawnPoint;
    public GameObject player;
    public GameObject prefab;

    private float timer;

    private void Start()
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
        prefab = GameObject.FindGameObjectWithTag("Prefab");
    }

    // Permet a l'ennemi de tirer en direction du Joueur
    private void Update()
    {
        if (objectGravity.isDestroy == true)
        {
            Respawn();
        }
    }

    // Permet a l'ennemi de faire apparaitre un projectile
    private void Respawn()
    {
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
