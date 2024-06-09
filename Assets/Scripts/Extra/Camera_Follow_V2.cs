using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow_V2 : MonoBehaviour
{
    public bool cameraIsFollowingPlayer = true;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.10f;
    private Vector3 velocity = Vector3.zero;

    private Transform target;
    private GameObject targetGO;

    private void Start()
    {
        targetGO = GameObject.FindGameObjectWithTag("Player");
        target = targetGO.transform;
    }
    private void Update()
    {
        if (cameraIsFollowingPlayer)
        {
            CameraFollow();
        }
    }

    private void CameraFollow()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
