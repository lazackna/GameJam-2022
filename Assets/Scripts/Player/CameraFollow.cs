using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] public Vector3 offset;

    [SerializeField] public bool hasPerspective;
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        Transform cameraTransform = transform;
        
        if (hasPerspective)
        {
            Transform transform1;
            cameraTransform.LookAt(target);
            cameraTransform.position = smoothedPosition;
        }
        else
        {
            var pos = cameraTransform.position;
            cameraTransform.position = new Vector3(smoothedPosition.x, pos.y, pos.z);
        }

    }

    
}
