using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>RechargableObject</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class RechargableObject : MonoBehaviour
{


    [SerializeField] private float electricalCost = 1f;
    [SerializeField] private bool active = false;

    [SerializeField] private float maxElectricalLevel = 100;
    [SerializeField] private float currentElectricalLevel = 0;
    [SerializeField] private float rechargeRate = 10f;
    [SerializeField] private float disrechargeRate = 4f;

    [SerializeField] private List<Sprite> chargeSprites;
    [SerializeField] private GameObject battery;

    bool charge = false;
    private SpriteRenderer batterySr;


    PlayerElectricity playerElectricity;

    public bool Active { get => active; }
    public float CurrentElectricalLevel { get => currentElectricalLevel; }

    private void Awake()
    {
        batterySr = battery.GetComponent<SpriteRenderer>();
        playerElectricity = FindObjectOfType<PlayerElectricity>();
    }

    private void Update()
    {
        if (charge)
        {
            Recharge();
        }
        else
        {
            Discharge();
        }
        
        battery.GetComponent<SpriteRenderer>().sprite = chargeSprites[(int)(currentElectricalLevel/17)];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            charge = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            charge = false;
        }      
    }

    /// <summary>
    /// <c>Recharge</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    private void Recharge()
    {
        playerElectricity.DecrementEL(electricalCost);
        active = true;
        currentElectricalLevel += 1 * rechargeRate * Time.deltaTime;
        if(currentElectricalLevel >= 100)
        {
            currentElectricalLevel = 100;
        }
     
    }

    /// <summary>
    /// <c>Discharge</c> is deprecated.
    /// DO NOT USE!!!
    /// </summary>
    private void Discharge()
    {
        if(CurrentElectricalLevel <= 0) 
        {
            currentElectricalLevel = 0;
            active = false;
        }
        else
        {
            active = true;
            currentElectricalLevel -= 1 * disrechargeRate * Time.deltaTime;
        }
    }

}
