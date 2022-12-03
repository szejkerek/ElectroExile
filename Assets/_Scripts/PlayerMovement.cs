using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float droneRangeOuter;
    [SerializeField] private float droneRangeInner;

    Rigidbody2D rb;
    Vector3 mousePosition;
    float distance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
            HorizontalMovement();
        }

        if (isMouseTop())
        {
        }

        if (isMouseDown())
        {
        }
       
    }

    private void HorizontalMovement()
    {
        Vector2 dir = mousePosition - transform.position;
        rb.velocity = new Vector2(dir.x * speed * Time.fixedDeltaTime, 0);
        foreach (Transform child in transform)
        {
            if (child.GetComponent<PlayerColliderCheck>().isColliding)
            {
                Debug.Log(child.name);
            }
        }
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
