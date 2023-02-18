using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>GroundCheck</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class GroundCheck : MonoBehaviour
{
    // Start is called before the first frame update

    public bool isColliding = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
         isColliding = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
         isColliding = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         isColliding = false;
    }
}
