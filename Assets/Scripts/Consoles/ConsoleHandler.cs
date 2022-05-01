using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleHandler : MonoBehaviour
{
    private Vector3 cameraPositions2d = new Vector3(0, -0.5f, -6);
    private Vector3 cameraPositions3d = new Vector3(-6, 2, 0);

    [SerializeField] private CameraFollow camera;

    [SerializeField] private Transform player;

    [SerializeField] private Transform playerModel;    
    [SerializeField]private GameObject DsRoot;
    private bool dsOn = false;

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
            if (dsOn)
            {
                DsRoot.SetActive(false);
                Camera.main.rect = new Rect(0, 0, 1, 1);
                dsOn = false;
            }
            //switch mode from 2d to 3d
            if (is2d)
            {
                is2d = false;
                player.rotation = Quaternion.Euler(0, 90, 0);
                playerModel.rotation = Quaternion.Euler(0, 90, 0);
                camera.offset = cameraPositions3d;
            }
            else
            {
                is2d = true;
                player.rotation = Quaternion.Euler(0, 0, 0);
                
                camera.offset = cameraPositions2d;
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (dsOn)
            {
                DsRoot.SetActive(false);
                Camera.main.rect = new Rect(0, 0, 1, 1);
                dsOn = false;
            }
            else
            {
                if (!is2d)
                {
                    is2d = true;
                    player.rotation = Quaternion.Euler(0, 0, 0);
                    camera.offset = cameraPositions2d;
                }

                DsRoot.SetActive(true);
                Camera.main.rect = new Rect(0, 0.5f, 1, 0.5f);
                
                dsOn = true;
            }
        }
    }
}
