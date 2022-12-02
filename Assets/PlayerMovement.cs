using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private float horizontalMovement;

    [SerializeField]
    private float speed;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();  
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        Debug.Log(horizontalMovement);
    }

}
