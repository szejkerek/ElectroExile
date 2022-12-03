using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikesBehavior : PlayerManager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag =="Player")
        {
            FindObjectOfType<PlayerManager>().KillPlayer();
        }
    }
}
