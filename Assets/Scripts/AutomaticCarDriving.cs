using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutomaticCarDriving : CarController
{
    private UnityEvent terrainReady;
    private void Start()
    {
        terrainReady = TerrainLoader.Instance.terrainReady;
        terrainReady.AddListener(DropCar);
    }

    void DropCar()
    {
        carRigidbody.useGravity = true;
    }

    public override void HandleDriving()
    {
        var maxDriveForce = driveForce * (1-(carRigidbody.velocity.magnitude/maxVelocity));

        frontLeftWheelCollider.motorTorque = -1 * maxDriveForce;
        frontRightWheelCollider.motorTorque = -1 * maxDriveForce;
    }

    private void Update()
    {
        CheckIfDrive();
        RollWindows();
    }
    
    //fix later
    void CheckIfDrive()
    {
        //if (GameController.Instance.promote == true && GameController.Instance.passenger == true)
        if (GameController.Instance.driver)
        {
            gameObject.GetComponent<AutomaticCarDriving>().enabled = false;
            gameObject.GetComponent<CarController>().enabled = true;

        }
    }
    
}
