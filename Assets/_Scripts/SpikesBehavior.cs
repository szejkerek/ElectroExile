using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikesBehavior : PlayerManager
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("lol");
        if (collision.gameObject.tag == "Player")
        {
            //funkcja do wpisania od playera
            Debug.Log("nice");
            KillPlayer();   
        }

        if (collision.gameObject.tag == "Enemy")
        {
            var box = collision.gameObject.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), box);
        }
    }
}
