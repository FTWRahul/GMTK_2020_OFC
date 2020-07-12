using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraShake : MonoBehaviour {
 
    public bool camShakeAcive = true; //on or off
    [Range(0, 1)]
    [SerializeField] float trauma;

    private CameraShakeSettings _settings;
    [SerializeField] private CameraShakeSettings defaultSettings;

    public CameraShakeSettings ShakeSettings
    {
        get
        {
            return _settings;
        }
        set
        {
            _settings = value;
        }
    }
    
    float timeCounter = 0; //counter stored for smooth transition

    [SerializeField]
    private UnityEvent onTraumaAdded;
    public float Trauma //accessor is used to keep trauma within 0 to 1 range
    {
        get
        {
            return trauma;
        }
        set
        {
            trauma = Mathf.Clamp01(value);
            onTraumaAdded?.Invoke();
        }
    }

    public float Decay
    {
        get
        {
            return defaultSettings.traumaDecay;
        }
        set
        {
            defaultSettings.traumaDecay = value;
        }
    }

    private void Awake()
    {
        ShakeSettings = defaultSettings;
    }

    //Get a perlin float between -1 & 1, based off the time counter.
    float GetFloat(float seed)
    {
        return (Mathf.PerlinNoise(seed, timeCounter) - 0.5f) * 2f;
    }
 
    //use the above function to generate a Vector3, different seeds are used to ensure different numbers
    Vector3 GetVec3()
    {
        return new Vector3(
            GetFloat(1),
            GetFloat(10),
            //deapth modifier applied here
            GetFloat(100) * _settings.traumaDepthMag
            );
    }
    
    private void Update ()
    {
        if (camShakeAcive && Trauma > 0)
        {
            //increase the time counter (how fast the position changes) based off the traumaMult and some root of the Trauma
            timeCounter += Time.deltaTime * Mathf.Pow(Trauma,0.3f) * _settings.traumaMult;
            //Bind the movement to the desired range
            Vector3 newPos = GetVec3() * (_settings.traumaMag * Trauma);;
            transform.localPosition = newPos;
            //rotation modifier applied here
            transform.localRotation = Quaternion.Euler(newPos * _settings.traumaRotMag);
            //decay faster at higher values
            Trauma -= Time.deltaTime * _settings.traumaDecay * (Trauma + 0.3f);
        }
        else
        {
            //lerp back towards default position and rotation once shake is done
            Vector3 newPos = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
            transform.localPosition = newPos;
            transform.localRotation = Quaternion.Euler(newPos * _settings.traumaRotMag);
        }
    }
}