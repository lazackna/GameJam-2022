using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    [SerializeField] private Vector3 cameraPositions2d;
    [SerializeField] private Vector3 cameraPositions3d;

    [SerializeField] private Vector2 cameraBounds;
    [SerializeField] private float smoothSpeed = 0.25f;

    [SerializeField] public bool hasPerspective;
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + (hasPerspective ? cameraPositions3d : cameraPositions2d);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        if (hasPerspective)
            transform.LookAt(target);
        
        transform.position = smoothedPosition;

        CheckCameraBounds();
    }

    private void CheckCameraBounds()
    {
        // Only 2D camera uses bounds
        if (hasPerspective) return;
        
        var pos = transform.position;
        
        // Checking x and y bounds
        if (pos.y < cameraBounds.y) transform.position = new Vector3(pos.x, cameraBounds.y, pos.z);
        if (pos.x < cameraBounds.x) transform.position = new Vector3(cameraBounds.x, cameraBounds.y, pos.z);
    }
}
