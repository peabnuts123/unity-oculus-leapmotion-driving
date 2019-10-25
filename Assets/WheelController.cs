using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    public WheelCollider[] poweredWheels;
    public float motorTorque = 1000;

    // Start is called before the first frame update
    public void Start()
    {
        foreach (WheelCollider wheel in this.poweredWheels)
        {
            wheel.motorTorque = this.motorTorque;
        }
    }
}
