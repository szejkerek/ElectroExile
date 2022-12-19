using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RechargableLight : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] float thresholdLight;

    [SerializeField] RechargableObject ro;
    [SerializeField] Light2D light;

    private void Start()
    {
        light.intensity = 0;
    }

    private void Update()
    {
        float lightDensity = ro.CurrentElectricalLevel/25; //<- normalized value
        light.intensity = lightDensity;

    }
}
