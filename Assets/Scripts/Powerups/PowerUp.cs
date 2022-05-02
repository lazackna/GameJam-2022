using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    [SerializeField] private float totalTime;
    [SerializeField] private GameObject player;
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private PowerUpHandler handler;


    // Update is called once per frame
    protected void Update()
    {
        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0, Space.World);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.Equals(player)) return;
        
        handler.OnPowerUpTrigger(powerUpType, totalTime);
        
        Debug.Log("Player hit power-up" + powerUpType);
    }

}
