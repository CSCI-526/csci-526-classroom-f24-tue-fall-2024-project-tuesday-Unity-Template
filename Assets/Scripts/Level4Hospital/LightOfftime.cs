using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOfftime : MonoBehaviour
{
    public Light lightSource; 
    public float toggleInterval = 5f; 

    private bool isLightOn = false; 

    void Start()
    {
        if (lightSource != null)
        {
            lightSource.enabled = isLightOn;
        }

        InvokeRepeating("ToggleLight", toggleInterval, toggleInterval);
    }

    void ToggleLight()
    {
        if (lightSource != null)
        {
            isLightOn = !isLightOn;
            lightSource.enabled = isLightOn;
        }
    }
}
