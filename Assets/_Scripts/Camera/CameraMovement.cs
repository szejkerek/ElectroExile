using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0f,6f)]
    [field: SerializeField] private float smoothing = 5f;

    private Transform target;
    void Awake()
    {
        target = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);
    }
}
