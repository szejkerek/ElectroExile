using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        StartCoroutine(KillingPlayerAnim());
       
    }

    private IEnumerator KillingPlayerAnim()
    {
        Debug.Log(GameObject.Find("Loader"));
        Animator animator = GameObject.Find("Loader").GetComponent<Animator>();
        animator.SetBool("Loading", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            KillPlayer();
        }
    }
}
