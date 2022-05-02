using System;using System.Collections;
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

    [SerializeField] private Text DSText;
    [SerializeField] private Text N64Text;
    
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
        
        powerUpObject = new Dictionary<PowerUpType, Text>
        {
            { PowerUpType.DS_CONSOLE, DSText },
            { PowerUpType.N64_CONSOLE, N64Text }
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (activePowerUp == PowerUpType.SWITCH_CONSOLE) return;

        powerUpTime[activePowerUp] -= Time.deltaTime;
        
        if (powerUpTime[activePowerUp] < 0) DrawPowerUps(activePowerUp);
    }

    public void OnPowerUpTrigger(PowerUpType powerUpType, float time)
    {
        powerUpTime[powerUpType] = time;

        DrawPowerUps(powerUpType);
    }

    private void DrawPowerUps(PowerUpType type)
    {
        powerUpObject[type].text = (int)powerUpTime[type] + "";
        
        powerUpObject[type].gameObject.SetActive(powerUpTime[type] >= 0);
    }
}
