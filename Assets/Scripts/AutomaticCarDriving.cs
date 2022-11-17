using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCarDriving : CarController
{
    public override void HandleDriving()
    {
        frontLeftWheelCollider.motorTorque = 1 * driveForce;
        frontRightWheelCollider.motorTorque = 1 * driveForce;
    }
}
