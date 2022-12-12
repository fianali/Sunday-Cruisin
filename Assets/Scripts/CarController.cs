using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] public WheelCollider frontLeftWheelCollider;
    [SerializeField] public WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider backLeftWheelCollider;
    [SerializeField] private WheelCollider backRightWheelCollider;
    
    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform backLeftWheelTransform;
    [SerializeField] private Transform backRightWheelTransform;
    
    [SerializeField] public float driveForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] protected float maxVelocity;
    [SerializeField] protected Rigidbody carRigidbody;

    public Transform rightWindow;
    [SerializeField] private Transform leftWindow;
    
    private float horizontalInput;
    private float verticalInput;
    private float currentBreakForce;
    private bool isBreaking;
    private float currentSteerAngle;

    
    // Start is called before the first frame update
    void Start()
    {
        // frontLeftWheelCollider.motorTorque = -1 * driveForce;
        // frontRightWheelCollider.motorTorque = -1 * driveForce;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetInputs();
        HandleDriving();
        HandleSteering();
        UpdateWheels();
    }

    private void Update()
    {
        RollWindows();
    }

    void GetInputs()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    public virtual void HandleDriving()
    {
        var maxDriveForce = driveForce * (1-(carRigidbody.velocity.magnitude/maxVelocity));
        frontLeftWheelCollider.motorTorque = verticalInput * maxDriveForce;
        frontRightWheelCollider.motorTorque = verticalInput * maxDriveForce;
        currentBreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplyBrakes();
            return;
        }
    }

    void ApplyBrakes()
    {
        frontLeftWheelCollider.brakeTorque = currentBreakForce;
        frontRightWheelCollider.brakeTorque = currentBreakForce;
        backLeftWheelCollider.brakeTorque = currentBreakForce;
        backRightWheelCollider.brakeTorque = currentBreakForce;
    
    }

    void HandleSteering()
    {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(backLeftWheelCollider, backLeftWheelTransform);
        UpdateSingleWheel(backRightWheelCollider, backRightWheelTransform);
    }

    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 localPosition;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out localPosition, out rotation);
        wheelTransform.localPosition = localPosition;
        wheelTransform.rotation = rotation;
    }

    void RollWindows()
    {

        if (rightWindow.localPosition.y >= -6.10 && leftWindow.localPosition.y >= -6.10)
        {
            Debug.Log(rightWindow.localPosition.y);
            Debug.Log("can roll down window");
            if (Input.GetKey(KeyCode.L))
            {
                rightWindow.localPosition += new Vector3(0f, -.05f);
                leftWindow.localPosition += new Vector3(0f, -.05f);
            }
            
        }


        if (rightWindow.localPosition.y <= 0 && leftWindow.localPosition.y <= 0)
        {
            Debug.Log("roll up window");
            if (Input.GetKey(KeyCode.O))
            {
                rightWindow.localPosition += new Vector3(0, .05f);
                leftWindow.localPosition += new Vector3(0, .05f);
            }
            
        }
    }
}
