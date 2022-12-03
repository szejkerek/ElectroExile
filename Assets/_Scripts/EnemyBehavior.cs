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
    [SerializeField] private float timeOfAbsorb = 3;
    [SerializeField] private Transform positionToMove;
    private Vector3 startPosition;
    private Rigidbody2D rb;
    private Vector3 offset = new Vector3(0.4f,-0.2f,0f);
    private Vector3 parentOffset = new Vector3(0.6f,0f,0f);

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
            Vector2 dire2 =  positionToMove.position - transform.position;
            Vector2 dire = startPosition - transform.position;
            Debug.Log($"{dire2.magnitude}, {dire.magnitude}");
            if (dire.magnitude < 0.05f)
            {
               
                Vector2 direction = positionToMove.position - transform.position;
                Vector2 newVector = direction.normalized * speed;
                rb.velocity = newVector;
                this.transform.localScale = new Vector3(1, 1, 1);
            }
            else if (dire2.magnitude < 0.05f)
            {
                // move right
                Vector2 direction = startPosition - transform.position;
                Vector2 newVector = direction.normalized * speed;
                rb.velocity = newVector;
                this.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        if (timeOfAbsorb < 0)
        {
            OnDestrution();
        }
    }

    private void OnDestrution()
    {
        timeOfAbsorb = -1;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 10;
        rb.gravityScale = 1;
        transform.SetParent(null);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isMoving = false;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" && timeOfAbsorb > 0)
        {
            var target = collider.gameObject.transform;
            if (target.position.x > transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position - target.right - offset,
                    speed * Time.deltaTime);
                if (transform.position.x >= target.position.x - target.right.x - parentOffset.x)
                {
                    transform.SetParent(target);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position + target.right + offset,
                    speed * Time.deltaTime);
                if (transform.position.x <= target.position.x + target.right.x + parentOffset.x)
                {
                    transform.SetParent(target);
                }
            }
            timeOfAbsorb -= Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("lmao");

        if (collision.gameObject.tag == "Player" && timeOfAbsorb < 0)
        {
            var lol = collision.gameObject.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(),lol);
        }
        OnDestrution();
    }
}
