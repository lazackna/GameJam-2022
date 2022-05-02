using System;
using System.Collections;
using System.Collections.Generic;
using Powerups;
using UnityEngine;

public class DSPowerUp : ConsolePowerUp
{
    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    protected override void PowerUpOn()
    {
        throw new System.NotImplementedException();
    }

    protected override void PowerUpOff()
    {
        throw new System.NotImplementedException();
    }

}
