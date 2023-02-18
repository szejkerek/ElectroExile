using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>EndDoorBehaviores</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class EndDoorBehaviores : MonoBehaviour
{
    void Start()
    {
    
    }
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (collider.gameObject.GetComponent<PlayerManager>().canWin == true)
            {
                Animator animator = GameObject.Find("Loader").GetComponent<Animator>();
                animator.SetBool("Loading", true);
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
        }
    }
}
