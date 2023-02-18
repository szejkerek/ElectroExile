using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// <c>SpikesBehavior</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class SpikesBehavior : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Collider2D>().tag =="Player")
        {
            FindObjectOfType<PlayerManager>().KillPlayer();
        }
    }
}
