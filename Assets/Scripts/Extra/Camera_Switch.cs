using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet de changer la camera principale avec une autre
public class Camera_Switch : MonoBehaviour
{
    [Header("Cameras")]
    private GameObject mainCam;
    private Camera_Follow_V2 cam;

    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] public Transform target;

    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        cam = mainCam.GetComponent<Camera_Follow_V2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cam.cameraIsFollowingPlayer = false;

            mainCam.transform.position = target.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        cam.cameraIsFollowingPlayer = true;
    }
}
