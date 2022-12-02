using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] bool isMoving;
    private bool movingLeft;


    // Start is called before the first frame update
    void Start()
    {
        if (isMoving)
        {
            movingLeft = false;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            if (movingLeft == true)
            {
                // move left
                if (transform.position.x <= -4) 
                    movingLeft = false;
                else if (transform.position.x >= 4)
                {
                    // move right
                    movingLeft = true;
                }
            }

        }


    }
}
