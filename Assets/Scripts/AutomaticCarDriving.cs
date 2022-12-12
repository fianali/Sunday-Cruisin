using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCarDriving : CarController
{
    public override void HandleDriving()
    {
        var maxDriveForce = driveForce * (1-(carRigidbody.velocity.magnitude/maxVelocity));

        frontLeftWheelCollider.motorTorque = -1 * maxDriveForce;
        frontRightWheelCollider.motorTorque = -1 * maxDriveForce;
    }

    private void Update()
    {
        CheckIfDrive();
    }
    
    //fix later
    void CheckIfDrive()
    {
        if (GameController.Instance.promote == true && GameController.Instance.passenger == true)
        {
            gameObject.GetComponent<AutomaticCarDriving>().enabled = false;
            gameObject.GetComponent<CarController>().enabled = true;

        }
    }
}
