using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoorBehaviores : MonoBehaviour
{
    void Start()
    {
        Animator animator = GetComponent<Animator>();
        animator.Play("Base Layer.OpenScene");
        
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        Animator animator = GetComponent<Animator>();
        animator.SetInteger("OnLevelEnd", 1);
        //animator.Play("Base Layer.CloseScene");
        Debug.Log("addbjobdkqwbi");
        //animacja
        //this.GetComponentInChildren<Animator>().Play(0,1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
    }
}
