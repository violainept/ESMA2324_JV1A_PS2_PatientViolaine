using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de donner un effet de parallaxe
public class Parallax_Effect : MonoBehaviour
{
    [Header("Parametres")]
    private float length, startpos;
    private GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    }

    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);
    }
}