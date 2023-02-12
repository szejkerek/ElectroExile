using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
