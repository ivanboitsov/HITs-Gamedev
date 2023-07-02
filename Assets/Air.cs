using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Air : MonoBehaviour
{
    public float AirActiveForce = 0.1f;
    public float AirForce = 0f;
    public bool active = false;
    public float changeDurationSec = 1f;
    public int id = 0;

    private void Start ()
    {
        AirForce = 0f;
        if (active)
        {
            AirForce = AirActiveForce;
        }
    }

    private void Update()
    {
        if (active && AirForce < AirActiveForce)
        {
            AirForce += AirActiveForce * (Time.deltaTime / changeDurationSec);
        }
        else if (!active && AirForce > 0f)
        {
            AirForce -= AirActiveForce * (Time.deltaTime / changeDurationSec);
        }
        //check if wrong
        if (AirForce < 0f)
        {
            AirForce = 0f;
        }
        if (AirForce > AirActiveForce)
        {
            AirForce = AirActiveForce;
        }
    }

    public float GetForce()
    {
        return AirForce*0.0002f;
    }

    public void SwitchState()
    {
        if (active)
        {
            active = false;
        }
        else
        {
            active = true;
        }
    }

    public int GetId()
    {
        return id;
    }
}
