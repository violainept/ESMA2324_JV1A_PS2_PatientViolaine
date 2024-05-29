using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Permet d'activer ou desactiver l'objet et de changer sa position
public class Spawner_Object_Boss : MonoBehaviour
{
    public GameObject objectGO; // "objectGO" pour "objectGameObject"
    public bool canDestroy = true;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boss") && canDestroy)
        {
            Spawn();
            canDestroy = false;
        }
    }

    // Fait apparaitre de nouveau l'objet
    public void Spawn()
    {
        objectGO.SetActive(true);
    }
}
