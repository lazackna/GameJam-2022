using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUpType
{
    DS_CONSOLE,
    N64_CONSOLE,
    SWITCH_CONSOLE
}

public class PowerUpHandler : MonoBehaviour
{
    private PowerUpType activePowerUp = PowerUpType.SWITCH_CONSOLE;

    private const int MaxValue = 20;
    
    [SerializeField] private ConsoleHandler callbackHandler;
  
    private Dictionary<PowerUpType, float> powerUpTime;
    private Dictionary<PowerUpType, Text> powerUpObject;

    // Start is called before the first frame update
    void Start()
    {
        powerUpTime = new Dictionary<PowerUpType, float>
        {
            { PowerUpType.DS_CONSOLE, 0 },
            { PowerUpType.N64_CONSOLE, 0 }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (activePowerUp == PowerUpType.SWITCH_CONSOLE) return;

        powerUpTime[activePowerUp] -= Time.deltaTime;

        if (powerUpTime[activePowerUp] <= 0)
        {
            if (activePowerUp == PowerUpType.N64_CONSOLE) callbackHandler.SwitchN64();
            if (activePowerUp == PowerUpType.DS_CONSOLE) callbackHandler.SwitchNintendo();
            activePowerUp = PowerUpType.SWITCH_CONSOLE;
        }
        
        DrawPowerUps(activePowerUp);
    }

    public void OnPowerUpTrigger(PowerUpType powerUpType, float time)
    {
        powerUpTime[powerUpType] = time;

        DrawPowerUps(powerUpType);
    }

    public bool OnPowerUpCall(PowerUpType requestedType)
    {
        if (powerUpTime[requestedType] <= 0) return false;

        activePowerUp = activePowerUp == requestedType ? PowerUpType.SWITCH_CONSOLE : requestedType;

        return true;
    }

    private void DrawPowerUps(PowerUpType type)
    {
        if(type == PowerUpType.SWITCH_CONSOLE) return;

        if(type == PowerUpType.DS_CONSOLE) UI_Handler.SetDSView((int)powerUpTime[type] > 0, (powerUpTime[type] / MaxValue) * 100);
        if(type == PowerUpType.N64_CONSOLE) UI_Handler.SetN64View((int)powerUpTime[type] > 0, (powerUpTime[type] / MaxValue) * 100);
    }
}