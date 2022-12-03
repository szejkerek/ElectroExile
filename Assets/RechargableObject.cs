using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargableObject : MonoBehaviour
{
    [SerializeField] private float maxElectricalLevel = 100;
    [SerializeField] private float currentElectricalLevel;
    [SerializeField] private float timeToZeroEL = 5f;

    float timeToZeroDefault;

    private void Awake()
    {
        timeToZeroDefault = timeToZeroEL;
    }


}
