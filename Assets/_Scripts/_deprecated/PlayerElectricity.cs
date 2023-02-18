using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// <c>PlayerElectricity</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class PlayerElectricity : MonoBehaviour
{
    [SerializeField] private float maxElectricity = 100;
    [SerializeField] private float electricityLevel;
    [SerializeField] private TextMeshProUGUI text;
    [Space]
    [SerializeField] private Animator animator;

    private void Start()
    {
        RestoreEL();
        animator = GetComponent<Animator>();
    }
    public float ElectricityLevel { get => electricityLevel;  }

    /// <summary>
    /// <c>RestoreEL</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void RestoreEL()
    {
        electricityLevel = maxElectricity;
    }

    /// <summary>
    /// <c>ZeroEL</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void ZeroEL()
    {
        electricityLevel = 0;
    }

    /// <summary>
    /// <c>DecrementEL</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void DecrementEL(float value)
    {
        electricityLevel -= value * Time.deltaTime;
        if(electricityLevel <= 0)
        {
            electricityLevel = 0;
            GetComponent<PlayerManager>().KillPlayer();
        }
    }

    /// <summary>
    /// <c>IncrementEL</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    public void IncrementEL(float value)
    {
        electricityLevel += value * Time.deltaTime;
        if (electricityLevel >= maxElectricity)
        {
            electricityLevel = maxElectricity;
        }
    }
    private void Update()
    {
        text.text = $"{Mathf.Round(electricityLevel)}%";
        animator.SetFloat("Energy", electricityLevel);
    }

}
