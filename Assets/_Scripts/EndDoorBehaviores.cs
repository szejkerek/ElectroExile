using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndDoorBehaviores : MonoBehaviour
{
    void Start()
    {
    
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        
        Debug.Log(GameObject.Find("Loader"));
        Animator animator = GameObject.Find("Loader").GetComponent<Animator>();
        animator.SetBool("Loading",true);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
    }
}
