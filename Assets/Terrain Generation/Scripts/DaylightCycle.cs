using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycle : MonoBehaviour
{
    [SerializeField] private float dayLength;

    private float _dayTimer;

    private Quaternion _sunRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        _dayTimer = 0; 
        _sunRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_dayTimer < dayLength) _dayTimer += Time.deltaTime;
        else _dayTimer = 0;

        var dayPercent = _dayTimer / dayLength;
        var sunPosition = 360 * dayPercent;
        transform.rotation = Quaternion.Euler(new Vector3(sunPosition, -30f, 0f));
    }
}
