using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class NewPlayerMovement : MonoBehaviour
{
    [SerializeField] private float electricityCost = 1f;

    [SerializeField] private MetalCollisionCheck leftCollider;
    [SerializeField] private MetalCollisionCheck rightCollider;
    [SerializeField] private MetalCollisionCheck topCollider;
    [SerializeField] private GroundCheck bottomCollider;
    [Space]
    [SerializeField] private float speed;
    [Space]
    [SerializeField] private Animator animator;

    PlayerElectricity playerElectricity;
    Rigidbody2D rb;
    Vector3 mousePosition;
    Vector3 playerPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerElectricity = GetComponent<PlayerElectricity>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
    }

    private void FixedUpdate()
    {
        mousePosition = Input.mousePosition;
        playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        HorizontalMovement();
        VerticalMovement();
        if (!bottomCollider.isColliding && (leftCollider.isColliding || rightCollider.isColliding || topCollider.isColliding)) 
            playerElectricity.DecrementEL(electricityCost);
        transform.localScale = new Vector3(rb.velocity.x > 0 ? 1 : -1, 1, 1);
    }

    private void HorizontalMovement()
    {

        Vector2 dir = (mousePosition - playerPosition);
               
        rb.velocity = new Vector2(dir.normalized.x * speed * Time.fixedDeltaTime, rb.velocity.y);
    }
    private void VerticalMovement()
    {
        if ((rightCollider.isColliding || leftCollider.isColliding) && MagnetInput())
        {
            rb.velocity = new Vector2(rb.velocity.x, mousePosition.y > playerPosition.y ? speed * Time.fixedDeltaTime : -speed * Time.fixedDeltaTime);
        }
    }

    private bool MagnetInput()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

}
