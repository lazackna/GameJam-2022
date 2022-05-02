using System;using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum PowerUpType
{
    DS_CONSOLE,
    N64_CONSOLE,
    SWITCH_CONSOLE
}

public class PowerUpHandler : MonoBehaviour
{

    private PowerUpType activePowerUp = PowerUpType.SWITCH_CONSOLE;

    private Dictionary<PowerUpType, float> powerUpTime;

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
        
        if (powerUpTime[activePowerUp] < 0) DrawPowerUps();
    }

    public void OnPowerUpTrigger(PowerUpType powerUpType, float time)
    {
        powerUpTime[powerUpType] = time;

        Debug.Log("TEST");
        
        DrawPowerUps();
    }

    private void DrawPowerUps()
    {
        //throw new NotImplementedException();
    }
}
