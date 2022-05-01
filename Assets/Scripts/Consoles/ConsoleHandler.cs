using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleHandler : MonoBehaviour
{
    private Vector3 cameraPositions2d = new Vector3(0, 2, -6);
    private Vector3 cameraPositions3d = new Vector3(-6, 2, 0);

    [SerializeField] private CameraFollow camera;

    [SerializeField] private Transform player;

    public bool is2d = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //switch mode from 2d to 3d
            if (is2d)
            {
                is2d = false;
                player.rotation = Quaternion.Euler(0, 90, 0);
                camera.offset = cameraPositions3d;
            }
            else
            {
                is2d = true;
                player.rotation = Quaternion.Euler(0, 0, 0);
                camera.offset = cameraPositions2d;
            }
        }
    }
}