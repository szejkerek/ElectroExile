using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public void KillPlayer()
    {
        StartCoroutine(KillingPlayerAnim());
    }

    private IEnumerator KillingPlayerAnim()
    {
        GetComponent<PlayerMovement>().enabled = false; //Trun off movement
        GetComponent<PlayerElectricity>().ZeroEL(); //Zero batery
        yield return new WaitForSeconds(2.5f); ;
        // Wygaœ ekran
        // Zresetuj scene 
    }
}
