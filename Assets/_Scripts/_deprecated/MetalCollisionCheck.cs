using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>MetalCollisionCheck</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class MetalCollisionCheck : MonoBehaviour
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
