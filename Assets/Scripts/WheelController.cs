using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [NotNull]
    public WheelCollider[] poweredWheels;
    [NotNull]
    public new Rigidbody rigidbody;
    public float motorTorque = 1000;
    public float maxSpeed = 3;


    // Start is called before the first frame update
    public void Update()
    {
        bool isReversing = Input.GetButton("Reverse");

        foreach (WheelCollider wheel in this.poweredWheels)
        {
            if (CurrentSpeed() < this.maxSpeed)
            {
                if (isReversing)
                {
                    wheel.motorTorque = -this.motorTorque;
                }
                else
                {
                    wheel.motorTorque = this.motorTorque;
                }
            }
            else
            {
                wheel.motorTorque = 0;
            }
        }
    }

    private float CurrentSpeed()
    {
        return this.rigidbody.velocity.magnitude;
    }
}
