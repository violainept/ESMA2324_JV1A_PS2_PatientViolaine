using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de faire apparaitre le Joueur a l'endroit du GameObject
public class Spawn_Player : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").transform.position = transform.position;
    }
}
