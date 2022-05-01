using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    
    public readonly Vector3 CameraPositions2d = new Vector3(3.5f, -2.5f, -6);
    public readonly Vector3 CameraPositions3d = new Vector3(-10, 3, 0);

    [SerializeField] private Vector2 cameraBounds = new Vector2(-2.0f, 4.0f);
    [SerializeField] private float smoothSpeed = 0.25f;

    [SerializeField] public bool hasPerspective;
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + (hasPerspective ? CameraPositions3d : CameraPositions2d);
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
        if (pos.x < cameraBounds.x) transform.position = new Vector3(cameraBounds.x, pos.y, pos.z);
    }
}
