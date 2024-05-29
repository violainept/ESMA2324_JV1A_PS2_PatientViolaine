using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de changer la camera principale avec une autre
public class Camera_Switch : MonoBehaviour
{
    [Header("Cameras")]
    public GameObject mainCam;
    public GameObject staticCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            mainCam.SetActive(false);
            staticCam.SetActive(true);
        }
    }
}
