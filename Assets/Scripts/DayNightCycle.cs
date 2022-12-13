// code from following this tutorial 
// https://timcoster.com/2020/02/26/unity-shadergraph-procedural-skybox-tutorial-pt-2-day-night-cycle/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class DayNightCycle : MonoBehaviour
{
    public static DayNightCycle instance;

    [Header("Time")]
    public float cycleInMinutes = 1;

    private float decimalTime = 0.0f;
    public float DecimalTime{get{return decimalTime;} private set{decimalTime = value;}}

    [Header("Sun")]

    public Transform sun;
    public Light sunLight;


    public AnimationCurve sunBrightness = new AnimationCurve(
        new Keyframe(0 , 0.01f),
        new Keyframe(0.15f, 0.01f),
        new Keyframe(0.35f, 1),
        new Keyframe(0.65f, 1),
        new Keyframe(0.85f, 0.01f),
        new Keyframe(1, 0.01f)
    );

    public Gradient sunColor = new Gradient(){
        colorKeys = new GradientColorKey[3]{
            new GradientColorKey(new Color(1, 0.75f, 0.3f), 0),
            new GradientColorKey(new Color(0.95f, 0.95f, 1), 0.5f),
            new GradientColorKey(new Color(1, 0.75f, 0.3f), 1),
        },
        alphaKeys = new GradientAlphaKey[2]{
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        }
    };

    [Header("Sky")]
    [GradientUsage(true)]
    public Gradient skyColorDay = new Gradient(){
        colorKeys = new GradientColorKey[3]{
            new GradientColorKey(new Color(0.75f, 0.3f, 0.17f), 0),
            new GradientColorKey(new Color(0.7f, 1.4f, 3), 0.5f),
            new GradientColorKey(new Color(0.75f, 0.3f, 0.17f), 1),
        },
        alphaKeys = new GradientAlphaKey[2]{
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        }
    };

    [GradientUsage(true)]
    public Gradient skyColorNight = new Gradient(){
        colorKeys = new GradientColorKey[3]{
            new GradientColorKey(new Color(0.75f, 0.3f, 0.17f), 0),
            new GradientColorKey(new Color(0.44f, 1, 1), 0.5f),
            new GradientColorKey(new Color(0.75f, 0.3f, 0.17f), 1),
        },
        alphaKeys = new GradientAlphaKey[2]{
            new GradientAlphaKey(1, 0),
            new GradientAlphaKey(1, 1)
        }
    };

    [Header("Clouds")]
    public Vector2 cloudsSpeed = new Vector2(1, -1);

    [Header("Time Of Day Events")]
    public UnityEvent onMidnight;
    public UnityEvent onMorning;
    public UnityEvent onNoon;
    public UnityEvent onEvening;

    private enum TimeOfDay{Night, Morning, Noon, Evening}

    private TimeOfDay timeOfDay = TimeOfDay.Night;
    private TimeOfDay TODMessageCheck = TimeOfDay.Night;

    private float sunAngle;


    void Awake()
    {
        if (DayNightCycle.instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Warning; Multiple instances found of {0}, only one instance of {0} allowed.", this);
        } 
    }

    void Start()
    {
        sun.rotation = Quaternion.Euler(0, -90, 0);
    }

    void Update()
    {
        UpdateSunAngle();

        if(Application.isPlaying)
        {
            UpdatedecimalTime();
            UpdateTimeOfDay();
            RotateSun();
            MoveClouds();
        }
        SetSunBrightness();
        SetSunColor();
        SetSkyColor();
    }


    void RotateSun()
    {
        sun.Rotate(Vector3.right * Time.deltaTime * 6 / cycleInMinutes);
    }

    void SetSunBrightness()
    {
        sunAngle = Vector3.SignedAngle(Vector3.down,sun.forward,sun.right); 
        sunAngle = sunAngle / 360 + 0.5f;
        sunLight.intensity = sunBrightness.Evaluate(sunAngle);
    }

    void SetSunColor()
    {
        sunLight.color = sunColor.Evaluate(sunAngle);
    }

    void UpdateSunAngle()
    {
        sunAngle = Vector3.SignedAngle(Vector3.down,sun.forward,sun.right);
        sunAngle = sunAngle/360+0.5f;
    }

    void SetSkyColor()
    {
        if(sunAngle >= 0.25f && sunAngle < 0.75f)
        {
            RenderSettings.skybox.SetColor("_SkyColor2",skyColorDay.Evaluate(sunAngle*2f-0.5f));
        }
        else if(sunAngle > 0.75f)
        {
            RenderSettings.skybox.SetColor("_SkyColorNight2",skyColorNight.Evaluate(sunAngle*2f-1.5f));
        }
        else
        {
            RenderSettings.skybox.SetColor("_SkyColorNight2",skyColorNight.Evaluate(sunAngle*2f+0.5f));
        }
    }

    void MoveClouds()
    {
        RenderSettings.skybox.SetVector("_CloudsOffset", (Vector2)RenderSettings.skybox.GetVector("_CloudsOffset") + Time.deltaTime * cloudsSpeed);
    }

    void UpdatedecimalTime()
    {
        decimalTime = (0.25f + Time.time * 6 / cycleInMinutes / 360)%1;
    }

    void UpdateTimeOfDay()
    {
        if(decimalTime > 0.25 && decimalTime < 0.5f)
        {
            timeOfDay = TimeOfDay.Morning;
        }
        else if(decimalTime > 0.5f && decimalTime < 0.75f)
        {
            timeOfDay = TimeOfDay.Noon;
        }
        else if(decimalTime > 0.75f)
        {
            timeOfDay = TimeOfDay.Evening;
        }
        else
        {
            timeOfDay = TimeOfDay.Night;
        }

        if(TODMessageCheck != timeOfDay)
        {
            InvokeTimeOfDayEvent();
            TODMessageCheck = timeOfDay;
        }
    }

    void InvokeTimeOfDayEvent()
    {
        switch (timeOfDay) {
            case TimeOfDay.Night:
                if (onMidnight != null) onMidnight.Invoke();
                break;
            case TimeOfDay.Morning:
                if(onMorning != null) onMorning.Invoke();
                break;
            case TimeOfDay.Noon:
                if (onNoon != null) onNoon.Invoke();
                break;
            case TimeOfDay.Evening:
                if(onEvening != null) onEvening.Invoke();
                break;
        }
    }
}