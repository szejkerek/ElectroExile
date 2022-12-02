using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float speed = 10f;
    private float dir = 0;
    private bool isTargeting;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        if (isMoving)
        {
            isTargeting = false;
            dir = 1;
        }
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            //transform.Translate(dir * speed * Time.deltaTime);
            rb.velocity = new Vector2( dir * speed ,0);
            if (isTargeting == false)
            {
                // move left
                if (transform.position.x <= -6)
                {
                    dir = 1;
                    rb.velocity = new Vector2(dir * speed , 0);
                }
                else if (transform.position.x >= 6)
                {
                    // move right
                    dir = -1;
                    rb.velocity = new Vector2(dir * speed , 0);
                }
            }

        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerHitbox")
        {
            isMoving = false;
            Debug.Log(collider.gameObject.name);
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerHitbox")
        { 
            //transform.LookAt(collider.gameObject.transform);
            var target = collider.gameObject.transform;
            if(target.position.x > transform.position.x)
                transform.position = Vector2.MoveTowards(transform.position, target.position - target.right, speed * Time.deltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position, target.position + target.right, speed * Time.deltaTime);

            // transform.position += transform.forward * speed * Time.deltaTime;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("lmao");
        if (collision.gameObject.tag == "PlayerHitbox")
        {
            rb.AddForce(collision.GetContact(0).normal * 20 );
        }
    }
}
