using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = 0.2f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minAngle = 40f;
    [SerializeField] float maxAngle = 50f;
    [SerializeField] float minIntensity = 0.3f;
    [SerializeField] float maxIntensity = 4f;
    bool isTurnedOn = false;
    
    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
        myLight.enabled = false;
    }

    private void Update()
    {
        TurnOnOffFlashlighter();
        if (isTurnedOn ==true)
        {
            DecreaseLightAngle();
            DecreaseLightIntensity();
        }
        else 
        {
            IncreaseLightAngle();
            IncreaseLightIntensity();
        }
    }

    private void TurnOnOffFlashlighter()
    {
        if (Input.GetKeyDown(KeyCode.F) && isTurnedOn == false)
        {            
            isTurnedOn = true;
            myLight.enabled = true;
        }

        else if (Input.GetKeyDown(KeyCode.F) && isTurnedOn == true)
        {
            isTurnedOn = false;
            myLight.enabled = false;
        }
        else return;

    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle <=minAngle)
        {           
            return;
        }
        else
        {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }       
    }
    private void IncreaseLightAngle()
    {
        if (myLight.spotAngle >= maxAngle)
        {
            return;
        }
        else
        {
            myLight.spotAngle += angleDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        if (myLight.intensity >= minIntensity)
        {
            myLight.intensity -= lightDecay * Time.deltaTime;
        }
        else
        {
            isTurnedOn = false;
            myLight.enabled = false;
        }
    }
    private void IncreaseLightIntensity()
    {
        if (myLight.intensity <= maxIntensity)
        {
            myLight.intensity += lightDecay * Time.deltaTime;
        }
        else return;
    }
}
