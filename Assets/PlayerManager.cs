using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private PlayerMovement varlM;
    private PlayerElectricity varlE;
    void Awake()
    {
        varlM = GetComponentInParent<PlayerMovement>();
        varlE = GetComponent<PlayerElectricity>();
    }


    public void KillPlayer()
    {
        Debug.Log("jestemtu");
        StartCoroutine(KillingPlayerAnim());
        
    }

    private IEnumerator KillingPlayerAnim()
    {
        Debug.Log("idnqwbodiqb");
        varlM.enabled = false; //Trun off movement
        varlE.ZeroEL(); //Zero batery
        yield return new WaitForSeconds(1.5f); ;
        // Wygaœ ekran
        // Zresetuj scene 
    }
}
