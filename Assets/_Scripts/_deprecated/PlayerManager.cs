using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public bool canWin = true;
    private PlayerMovement varlM;
    private PlayerElectricity varlE;
    void Awake()
    {
        varlM = GetComponentInParent<PlayerMovement>();
        varlE = GetComponent<PlayerElectricity>();
    }


    public void KillPlayer()
    {
        canWin = false;
        StartCoroutine(KillingPlayerAnim());     
    }

    private IEnumerator KillingPlayerAnim()
    {
        Animator animator = GameObject.Find("Loader").GetComponent<Animator>();
        animator.SetBool("Loading", true);
        yield return new WaitForSeconds(1.0f);
        canWin = true;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            KillPlayer();
        }
    }
}
