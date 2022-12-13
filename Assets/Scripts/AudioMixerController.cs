using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerController : MonoBehaviour
{

    public static AudioMixerController Instance;
    [SerializeField] public AudioMixer mixer;
    [SerializeField] private string drivingVolume;
    [SerializeField] private string drivingLowPass;
    [SerializeField] private string musicVolume;
    [SerializeField] public string musicReverb;
    [SerializeField] private string dialogueVolume;
    [SerializeField] private string dialogueHighpass;

    private float drivingVolumeValue;
    private float drivingLowpassValue;
    private float musicVolumeValue;
    public float musicReverbValue;
    private float dialogueVolumeValue;
    private float dialogueHighpassValue;

    [SerializeField] private float lerpValue = .075f;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        GetValues();
        ChangeDrivingAudio();
        ChangeMusicAudio();
        ChangeDialogueAudio();
    }

    void GetValues()
    {
        bool drivingVolumeResult =  mixer.GetFloat(drivingVolume, out drivingVolumeValue);
        bool drivingLowpassResult =  mixer.GetFloat(drivingLowPass, out drivingLowpassValue);
        
        bool musicVolumeResult =  mixer.GetFloat(musicVolume, out musicVolumeValue);
        bool musicReverbResult =  mixer.GetFloat(musicReverb, out musicReverbValue);

        bool dialogueVolumeResult =  mixer.GetFloat(dialogueVolume, out dialogueVolumeValue);
        bool dialogueHighpassResult =  mixer.GetFloat(dialogueHighpass, out dialogueHighpassValue);
        
    }

    void ChangeDrivingAudio()
    {
        if (Input.GetKey(KeyCode.L))
        {
            mixer.SetFloat(drivingVolume, Mathf.Lerp(drivingVolumeValue, 10f, lerpValue * Time.deltaTime));
            mixer.SetFloat(drivingLowPass,  Mathf.Lerp(drivingLowpassValue,22000f, lerpValue * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.O))
        {
            mixer.SetFloat(drivingVolume, Mathf.Lerp(drivingVolumeValue, -10f, lerpValue * Time.deltaTime));
            mixer.SetFloat(drivingLowPass,  Mathf.Lerp(drivingLowpassValue, 1000f, lerpValue * Time.deltaTime));
        }
    }

    void ChangeMusicAudio()
    {
        if (Input.GetKey(KeyCode.L))
        {
            mixer.SetFloat(musicVolume, Mathf.Lerp(musicVolumeValue, -5f, lerpValue * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.O))
        {
            mixer.SetFloat(musicVolume, Mathf.Lerp(musicVolumeValue, 10f, lerpValue * Time.deltaTime));
        }
    }
    
    void ChangeDialogueAudio()
    {
        if (Input.GetKey(KeyCode.L))
        {
            mixer.SetFloat(dialogueVolume, Mathf.Lerp(dialogueVolumeValue, -8f, lerpValue * Time.deltaTime));
            mixer.SetFloat(dialogueHighpass, Mathf.Lerp(dialogueHighpassValue, 2000f, lerpValue * Time.deltaTime));

        }
        if (Input.GetKey(KeyCode.O))
        {
            mixer.SetFloat(dialogueVolume, Mathf.Lerp(dialogueVolumeValue, 0f, lerpValue * Time.deltaTime));
            mixer.SetFloat(dialogueHighpass, Mathf.Lerp(dialogueHighpassValue, 10f, lerpValue * Time.deltaTime));

        }
    }
}
