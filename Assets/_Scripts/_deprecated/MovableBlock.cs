using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>MovableBlock</c> is deprecated.
/// DO NOT USE!!!
/// </summary>
public class MovableBlock : MonoBehaviour
{
    [SerializeField] RechargableObject ro;
    [SerializeField] Transform platform;
    [SerializeField] float endHeight;
    [SerializeField] float speed;

    private Vector2 startingPosition;

    private void Awake()
    {
        startingPosition = platform.position;
    }

    private void Update()
    {
        if (ro.Active)
        {
            MoveToDestination();
        }
        else
        {
            MoveToStart();
        }
    }

    void MoveToDestination()
    {
        platform.position = Vector2.Lerp(platform.position, new Vector2(startingPosition.x, startingPosition.y + endHeight), speed * Time.deltaTime);    
    }

    void MoveToStart()
    {
        platform.position = Vector2.Lerp(platform.position, startingPosition, speed* 2.5f * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + endHeight, 0.01f), new Vector3(1, 4f, 0.01f));
    }

}
