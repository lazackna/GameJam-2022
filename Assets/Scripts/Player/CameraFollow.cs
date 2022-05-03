using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 cameraPositions2d = new Vector3(2, -2.5f, -6);
    private Vector3 cameraPositions3d = new Vector3(-10, 3, 0);

    private Vector2 cameraBounds = new Vector2(-2, 5.5f);
    private float smoothSpeed = 0.25f;

    [SerializeField] public bool hasPerspective = true;
    
    private void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + (hasPerspective ? cameraPositions3d : cameraPositions2d);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        
        if (hasPerspective)
            transform.LookAt(target);
        
        transform.position = smoothedPosition;

        CheckCameraBounds();
    }

    public void Start()
    {
        //throw new NotImplementedException();
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
