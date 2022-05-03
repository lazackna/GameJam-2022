using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class ConsoleHandler : MonoBehaviour
{
    private readonly float orthographicSize = 6.0f;

    [SerializeField] private CameraFollow camera;
    [SerializeField] private PowerUpHandler handler;

    [SerializeField] private GameObject player;

    [SerializeField] private Transform playerModel;
    [SerializeField] private GameObject DsRoot;
    public GameObject dsSign;
    public GameObject switchSign;
    public GameObject n64Sign;

    private bool dsOn = false;

    public bool is2d = true;

    private Camera mainCamera;

    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        dsSign.SetActive(false);
        n64Sign.SetActive(false);
        switchSign.SetActive(true);
        is2d = true;
        player.transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y, 0);
        player.transform.rotation = Quaternion.Euler(0, 0, 0);
        playerModel.rotation = Quaternion.Euler(0, -90, 0);
        camera.hasPerspective = false;
        mainCamera.orthographic = true;
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        mainCamera.orthographicSize = orthographicSize;
        dsSign.SetActive(false);
        n64Sign.SetActive(false);
        switchSign.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            if (handler.OnPowerUpCall(PowerUpType.N64_CONSOLE))
                SwitchN64();


        if (Input.GetKeyDown(KeyCode.Q))
            if (handler.OnPowerUpCall(PowerUpType.DS_CONSOLE))
                SwitchNintendo();
    }

    public void SwitchN64()
    {
        if (dsOn)
        {
            DsRoot.SetActive(false);
            mainCamera.rect = new Rect(0, 0, 1, 1);
            dsOn = false;
        }


        //switch mode from 2d to 3d
        if (is2d)
        {
            is2d = false;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            camera.hasPerspective = true;
            mainCamera.orthographic = false;
            playerModel.rotation = Quaternion.Euler(0, 90, 0);
            playerModel.rotation = Quaternion.Euler(0, -90, 0);
            dsSign.SetActive(false);
            n64Sign.SetActive(true);
            switchSign.SetActive(false);
        }
        else
        {
            is2d = true;
            player.transform.position = new Vector3(this.player.transform.position.x, this.player.transform.position.y, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerModel.rotation = Quaternion.Euler(0, -90, 0);
            camera.hasPerspective = false;
            mainCamera.orthographic = true;
            mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
            mainCamera.orthographicSize = orthographicSize;
            dsSign.SetActive(false);
            n64Sign.SetActive(false);
            switchSign.SetActive(true);
        }
    }

    public void SwitchNintendo()
    {
        if (dsOn)
        {
            PlayerMotor.CanMove = true;
            
            DsRoot.SetActive(false);
            mainCamera.rect = new Rect(0, 0, 1, 1);
            dsOn = false;
            if (is2d)
            {
                dsSign.SetActive(false);
                n64Sign.SetActive(false);
                switchSign.SetActive(true);
            }
            else
            {
                dsSign.SetActive(false);
                n64Sign.SetActive(true);
                switchSign.SetActive(false);
            }
        }
        else
        {
            PlayerMotor.CanMove = false;
            
            if (!is2d)
            {
                is2d = true;
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
                camera.hasPerspective = false;
                mainCamera.orthographic = true;
                mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
                mainCamera.orthographicSize = orthographicSize;
            }

            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            dsSign.SetActive(true);
            n64Sign.SetActive(false);
            switchSign.SetActive(false);
            DsRoot.SetActive(true);
            mainCamera.rect = new Rect(0, 0.5f, 1, 0.5f);

            dsOn = true;
        }
    }
}