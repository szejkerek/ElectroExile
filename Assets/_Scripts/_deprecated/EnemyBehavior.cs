using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

/// <summary>
/// <c>EnemyBehavior</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private List<Transform> keyPoints = new List<Transform>();
    [SerializeField] private float drainPower = 1f;
    [SerializeField] private Sprite deadSprite;
    private int keyPointsIterator = 0;
    private Rigidbody2D rb;
    private Vector3 offset = new Vector3(0.4f,0.0f,0f);
    private Vector3 parentOffset = new Vector3(0.6f,0f,0f);

    // Start is called before the first frame updates
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.localScale = new Vector3(rb.velocity.x > 0 ? 1 : -1, 1, 1);
            Vector2 direction = keyPoints[keyPointsIterator].position - transform.position;
            if (direction.magnitude < 0.05f)
            {
                keyPointsIterator++;
                if (keyPointsIterator >= keyPoints.Count)
                    keyPointsIterator = 0;
                SetDirection();
                              
            }
        }
    }

    private void SetDirection()
    {
        Vector2 newDirection = keyPoints[keyPointsIterator].position - transform.position;
        rb.velocity = newDirection.normalized * speed;
    }

    private void OnDestrution()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.mass = 10;
        rb.gravityScale = 1;
        this.GetComponent<Animator>().enabled = false;
        transform.SetParent(null);
        this.GetComponent<SpriteRenderer>().sprite = deadSprite;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isMoving = true;
            SetDirection();
        }
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
        if (collider.gameObject.tag == "Player" )
        {
            var target = collider.gameObject.transform;
            if (target.position.x > transform.position.x)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position,
                    speed* acceleration * Time.deltaTime);
                if (transform.position.x >= target.position.x - target.right.x - parentOffset.x)
                {
                    transform.SetParent(target);
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position ,
                    speed* acceleration * Time.deltaTime);
                if (transform.position.x <= target.position.x + target.right.x + parentOffset.x)
                {
                    transform.SetParent(target);
                }
            }
            if (transform.IsChildOf(target))
            {
                StartCoroutine(deadEnumerator());
            }
        }
    }

    private IEnumerator deadEnumerator()
    {
        Animator animator = GameObject.Find("Loader").GetComponent<Animator>();
        animator.SetBool("Loading", true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // OnDestrution();
    }
}
