using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonPerspectiveController : MonoBehaviour
{

    // Variables
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    float cameraHorizontalRotation = 0f;

    bool lockedCursor = true;


    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    
    void Update()
    {
        // Collect Mouse Input

        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        // Rotate the Camera around its local X axis

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 35f);
        cameraHorizontalRotation -= inputX;
        cameraHorizontalRotation = Mathf.Clamp(cameraHorizontalRotation, -160f, 160f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        transform.localEulerAngles -= Vector3.up * cameraHorizontalRotation;

    }
}