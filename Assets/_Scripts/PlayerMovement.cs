using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    private float ElectricityDecrement = 1f;

    [SerializeField] private MetalCollisionCheck leftCollider; 
    [SerializeField] private MetalCollisionCheck rightCollider;
    [SerializeField] private MetalCollisionCheck topCollider;
    [SerializeField] private GroundCheck bottomCollider;
    [Space]
    [SerializeField] private float speed;
    [SerializeField] private float scrollMultiplier;
    [SerializeField] private float droneRangeOuter;
    [SerializeField] private float droneRangeInner;

    PlayerElectricity playerElectricity;
    Rigidbody2D rb;
    Vector3 mousePosition;
    float distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerElectricity = GetComponent<PlayerElectricity>();
    }

    private void FixedUpdate()
    {
        mousePosition = GetMousePosition();


        if (!IsMouseInMovementRange())
        {
            return;
        }

        if (isMouseLeft() || isMouseRight())
        {
            if (IsInTheAir())
                return;

            bool snapHead = TouchingHead() && HoldingMagneticKey();
            HorizontalMovement(snapHead);
        }

        if (isMouseTop() || isMouseDown())
        {
            if (TouchingSide() && HoldingMagneticKey())
            {     
                VerticalMovement();
                playerElectricity.DecrementEL(ElectricityDecrement);
            }
        }
       
    }

    private void HorizontalMovement(bool snapHead)
    {
        Vector2 dir = mousePosition - transform.position;
        Vector2 previousPos = transform.position;
        rb.velocity = new Vector2(dir.x * speed * Time.fixedDeltaTime, snapHead? -rb.velocity.y : rb.velocity.y);

        if (snapHead)
        {
            rb.AddForce(Vector2.up * 20, ForceMode2D.Force);
            playerElectricity.DecrementEL(ElectricityDecrement);
        }
    }

    private bool IsInTheAir()
    {
        return !bottomCollider.isColliding && !TouchingHead();
    }

    private bool TouchingSide()
    {
        return (leftCollider.isColliding || rightCollider.isColliding);
    }

    private bool TouchingHead()
    {
        return (topCollider.isColliding);
    }

    private bool HoldingMagneticKey()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    private void VerticalMovement()
    {
        Vector2 dir = mousePosition - transform.position;
        rb.velocity = new Vector2(rb.velocity.x, dir.y * speed * Time.fixedDeltaTime);
    }

    private bool IsMouseInMovementRange()
    {
        Vector2 mouse2D = mousePosition;
        Vector2 player2D = transform.position;

        distance = Vector2.Distance(mouse2D, player2D);

        return (distance <= droneRangeInner) && (distance >= droneRangeOuter);
    }

    private bool isMouseLeft()
    {
        return mousePosition.x <= (transform.position.x - droneRangeOuter);
    }

    private bool isMouseRight()
    {
        return mousePosition.x >= (transform.position.x + droneRangeOuter);
    }

    private bool isMouseTop()
    {
        return mousePosition.y >= (transform.position.y + droneRangeOuter) && (!isMouseLeft() && !isMouseRight());
    }

    private bool isMouseDown()
    {
        return mousePosition.y <= (transform.position.y - droneRangeOuter) && (!isMouseLeft() && !isMouseRight());
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, droneRangeInner);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, droneRangeOuter);
    }
}
