using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;


public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Transform positionToMove;
    private float dir = 0;
    private Vector3 startPosition;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            // move left
            if (transform.position == startPosition)
            {
                Vector2 direction = positionToMove.position - transform.position;
                Vector2 newVector = direction.normalized * speed;
                rb.velocity = newVector;
            }
            else if (transform.position == positionToMove.position)
            {
                // move right
                Vector2 direction = startPosition - transform.position;
                Vector2 newVector = direction.normalized * speed;
                rb.velocity = newVector;
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
