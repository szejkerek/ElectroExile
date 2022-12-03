using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D playerCollider;
    [SerializeField] private BoxCollider2D platformTrigger;
    [SerializeField] private Transform platform;
    [SerializeField] private Vector2 startingPlatformPositon;
    [SerializeField] private float height;
    [SerializeField] private float speed;

    bool moveUp = false;

    private void Update()
    {
        if(playerCollider.IsTouching(platformTrigger))
        {
            if (Vector2.Distance(platform.transform.position, startingPlatformPositon) >= 0.5f)
                return;

            moveUp = true;     
        }
        else
        {
            moveUp = false;
        }
    }

    private void FixedUpdate()
    {
        if (moveUp)
        {
            platform.transform.position = Vector2.Lerp(platform.transform.position, new Vector2(startingPlatformPositon.x, startingPlatformPositon.y + height), speed * Time.deltaTime);
        }
        else
        {
            platform.transform.position = Vector2.Lerp(platform.transform.position, startingPlatformPositon, speed * Time.deltaTime);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(startingPlatformPositon.x, startingPlatformPositon.y + height, 0.01f), new Vector3(4, 0.5f, 0.01f));
    }



}
