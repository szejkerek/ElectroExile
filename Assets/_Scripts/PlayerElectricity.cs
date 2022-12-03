using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerElectricity : MonoBehaviour
{
    [SerializeField] private float maxElectricity = 100;
    [SerializeField] private float electricityLevel;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        RestoreEL();
    }
    public float ElectricityLevel { get => electricityLevel;  }

    public void RestoreEL()
    {
        electricityLevel = maxElectricity;
    }

    public void ZeroEL()
    {
        electricityLevel = 0;
    }

    public void DecrementEL(float value)
    {
        electricityLevel -= value;
        if(electricityLevel <= 0)
        {
            electricityLevel = 0;
            GetComponent<PlayerManager>().KillPlayer();
        }
    }

    public void IncrementEL(float value)
    {
        electricityLevel += value;
        if (electricityLevel >= maxElectricity)
        {
            electricityLevel = maxElectricity;
        }
    }
    private void Update()
    {
        text.text = $"{Mathf.Round(electricityLevel)}%";
    }

}
