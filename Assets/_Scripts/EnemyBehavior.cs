using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float speed = 10f;
    private Vector3 dir = Vector3.left;
    private bool isTargeting;


    // Start is called before the first frame update
    void Start()
    {
        if (isMoving)
        {
            isTargeting = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            transform.Translate(dir * speed * Time.deltaTime);
            if (isTargeting == false)
            {
                // move left
                if (transform.position.x <= -4)
                {
                    dir = Vector3.right;
                }
                else if (transform.position.x >= 4)
                {
                    // move right
                    dir = Vector3.left;
                }
            }

        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            isMoving = false;
        }
        Debug.Log(collider.gameObject.name);
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        { 
            //transform.LookAt(collider.gameObject.transform);
            var target = collider.gameObject.transform;
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            // transform.position += transform.forward * speed * Time.deltaTime;


        }

    }

}
