using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAngle : MonoBehaviour
{
    // Public references
    [NotNull]
    public WheelCollider wheelCollider;
    [NotNull]
    public Transform wheelModel;

    public void Update()
    {
        this.wheelModel.localRotation = Quaternion.Euler(0, this.wheelCollider.steerAngle, 0);
    }
}
