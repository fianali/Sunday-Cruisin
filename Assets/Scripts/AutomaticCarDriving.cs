using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticCarDriving : CarController
{
    public override void HandleDriving()
    {
        frontLeftWheelCollider.motorTorque = -1 * driveForce;
        frontRightWheelCollider.motorTorque = -1 * driveForce;
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
