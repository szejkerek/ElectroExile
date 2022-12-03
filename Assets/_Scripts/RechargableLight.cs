using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargableLight : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float thresholdLight;

    [SerializeField] RechargableObject ro;
    private void Update()
    {
        float lightDensity = ro.CurrentElectricalLevel/100; //<- normalized value

        if(lightDensity > 0)
        {
            Debug.Log("Light value: " + lightDensity);
        }

    }
}
