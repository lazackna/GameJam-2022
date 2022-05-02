using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerUp : MonoBehaviour
{

    [SerializeField] private float totalTime;
    [SerializeField] protected GameObject player;
    
    private float timeLeft;
    private bool isActive;

    // Start is called before the first frame update
    protected void Start()
    {
        timeLeft = totalTime;
    }
    
    public bool ActivatePowerUp()
    {
        if (timeLeft < 0) return false;

        PowerUpOn();
        
        return (isActive = true);
    }

    public void DisablePowerUp()
    {
        PowerUpOff();
        
        isActive = false;
    }

    public bool IsActive()
    {
        return isActive;
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isActive) timeLeft -= Time.deltaTime;
        
        if (timeLeft < 0) DisablePowerUp();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.Equals(player)) return;
        
        Debug.Log("Player hit power-up");
    }

    protected abstract void PowerUpOn();
    protected abstract void PowerUpOff();
}
