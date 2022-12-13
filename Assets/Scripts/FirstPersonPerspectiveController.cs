using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class FirstPersonPerspectiveController : MonoBehaviour
{

    public static FirstPersonPerspectiveController Instance { get; private set; }
    
    // Variables
    public Transform player;
    public float mouseSensitivity = 2f;
    float cameraVerticalRotation = 0f;
    float cameraHorizontalRotation = 0f;

    bool lockedCursor = true;

    public GameObject minusVolume;
    public GameObject plusVolume;
    public GameObject minusReverb;
    public GameObject plusReverb;
    public GameObject minusPitch;
    public GameObject plusPitch;
    public GameObject lastStation;
    public GameObject nextStation;
    public GameObject lastSong;
    public GameObject nextSong;
    
    public Vector3 collision = Vector3.zero;
    
    private bool lookedAtWindow = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }

    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // minusVolume.name = "volume";
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
        
        
        
        /////////////////////////////////////////////////////////////////
        
        
        var ray = new Ray(this.transform.position, this.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            //change volume
            var volume = SoundController.Instance.radio.volume;
            if (hit.collider.gameObject.name == "+volume")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    volume += .10f;
                    SoundController.Instance.radio.volume = volume;
                }
            }
            
            if (hit.collider.gameObject.name == "-volume")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    volume += -.10f;
                    SoundController.Instance.radio.volume = volume;
                }
            }
            
            //change reverb
            var reverbString = AudioMixerController.Instance.musicReverb;
            var reverbValue = AudioMixerController.Instance.musicReverbValue;
            var reverbResult = AudioMixerController.Instance.mixer.GetFloat(reverbString, out reverbValue);
            if (hit.collider.gameObject.name == "+reverb")
            {
                
                if (Input.GetMouseButtonDown(0))
                {
                    AudioMixerController.Instance.mixer.SetFloat(reverbString, reverbValue + 1000f);
                }
            }
            
            if (hit.collider.gameObject.name == "-reverb")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (reverbValue >= -5000f)
                    {
                        AudioMixerController.Instance.mixer.SetFloat(reverbString, reverbValue + -1000f);
                    }
                }
            }
            
            //change pitch
            var pitch = SoundController.Instance.radio.pitch;
            if (hit.collider.gameObject.name == "+pitch")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pitch += .10f;
                    SoundController.Instance.radio.pitch = pitch;
                }
            }
            
            if (hit.collider.gameObject.name == "-pitch")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    pitch += -.10f;
                    SoundController.Instance.radio.pitch = pitch;
                }
            }
            
        }
        
        
        ////////////////////// Did they look at the window? /////////////////////////////

        
        // Debug.Log(Math.Abs(this.transform.rotation.y));
        if (lookedAtWindow == false)
        {
            
            if (Math.Abs(this.transform.rotation.y) < .32f)
            {
                AnimationTesting.Instance.LookedAtWindow();
                lookedAtWindow = true;
            }
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(collision, .2f);
        Vector3 direction = transform.TransformDirection(Vector3.forward) * 5000;
        Gizmos.DrawRay(this.transform.position, direction);
    }

    void ChangeVolume()
    {
        
    }
}