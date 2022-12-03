using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargableObject : MonoBehaviour
{


    [SerializeField] private float electricalCost = 1f;
    [SerializeField] private bool active = false;

    [SerializeField] private float maxElectricalLevel = 100;
    [SerializeField] private float currentElectricalLevel = 0;
    [SerializeField] private float rechargeRate = 10f;
    [SerializeField] private float disrechargeRate = 4f;

    bool charge = false;


    PlayerElectricity playerElectricity;

    public bool Active { get => active; }
    public float CurrentElectricalLevel { get => currentElectricalLevel; }

    private void Awake()
    {
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
