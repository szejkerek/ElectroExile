using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderCheck : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isColliding = false;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Metal")
        {
            isColliding = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        
        if (other.tag == "Metal")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Metal")
        {
            isColliding = false;
        }
    }
}
