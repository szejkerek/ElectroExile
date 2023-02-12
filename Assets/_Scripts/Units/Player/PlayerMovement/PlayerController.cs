using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CollisionVariables _collisionVariables;
    public bool IsGrounded { get => _isTouchingTerrain[TerrainTypes.Ground]; }

    private Rigidbody2D _rigidbody2D;
    private PlayerInputs _playerInputs;
    private bool _facingLeft = false;
    Dictionary<TerrainTypes, bool> _isTouchingTerrain = new Dictionary<TerrainTypes, bool>();

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GatherInputs();
        TurnPlayerInWalkingDirection();
        CheckForTerrainCollisions();
    }
    
    private readonly Collider2D[] _groundCollision = new Collider2D[1];
    private readonly Collider2D[] _leftWallCollision = new Collider2D[1];
    private readonly Collider2D[] _rightWallCollision = new Collider2D[1];
    private readonly Collider2D[] _roofCollision = new Collider2D[1];
    private Vector3 overlapOffset = new Vector3(0, 0, 0);
    private void CheckForTerrainCollisions()
    {
        overlapOffset.y = _collisionVariables.groundCheckOffset;
        _isTouchingTerrain[TerrainTypes.Ground] = CheckForOverlap(_collisionVariables.groundCheckRadius, _groundCollision);

        overlapOffset.y = _collisionVariables.roofCheckOffset;
        _isTouchingTerrain[TerrainTypes.Roof] = CheckForOverlap(_collisionVariables.roofCheckRadius,_roofCollision);

        overlapOffset.y = 0;
        overlapOffset.x = _collisionVariables.wallsCheckOffset;
        _isTouchingTerrain[TerrainTypes.RightWall] = CheckForOverlap(_collisionVariables.wallsCheckRadius,_groundCollision);

        overlapOffset.x = -_collisionVariables.wallsCheckOffset;
        _isTouchingTerrain[TerrainTypes.LeftWall] = CheckForOverlap(_collisionVariables.wallsCheckRadius, _groundCollision);
        overlapOffset.x = 0;

        bool CheckForOverlap(float checkRadius, Collider2D[] result)
        {
            return Physics2D.OverlapCircleNonAlloc(transform.position + overlapOffset, checkRadius, result, _collisionVariables.groundMask) > 0;
        }
    }

    void GatherInputs()
    {
        _playerInputs.RawX = (int)Input.GetAxisRaw("Horizontal");
        _playerInputs.RawY = (int)Input.GetAxisRaw("Vertical");
        _playerInputs.X = Input.GetAxis("Horizontal");
        _playerInputs.Y = Input.GetAxis("Vertical");
    }

    void TurnPlayerInWalkingDirection()
    {
        _facingLeft = _playerInputs.RawX != 1 && (_playerInputs.RawX == -1 || _facingLeft);

        if (_facingLeft)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    #region Gizmos
    void DrawTerrainCollisionsGizmos()
    {
        if (!_collisionVariables.drawGizmos)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, _collisionVariables.groundCheckOffset), _collisionVariables.groundCheckRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, _collisionVariables.roofCheckOffset), _collisionVariables.roofCheckRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(_collisionVariables.wallsCheckOffset, 0), _collisionVariables.wallsCheckRadius);
        Gizmos.DrawWireSphere(transform.position + new Vector3(-_collisionVariables.wallsCheckOffset, 0), _collisionVariables.wallsCheckRadius);
    }

    private void OnDrawGizmos()
    {
        DrawTerrainCollisionsGizmos();
    }
    #endregion

}

#region Structures

[System.Serializable]
public struct CollisionVariables
{
    public bool drawGizmos;
    public LayerMask groundMask;
    [Space]
    public float groundCheckOffset;
    public float groundCheckRadius;
    [Space]
    public float wallsCheckOffset;
    public float wallsCheckRadius;
    [Space]
    public float roofCheckOffset;
    public float roofCheckRadius;
}

[System.Serializable]
struct PlayerInputs
{
    public float X, Y;
    public int RawX, RawY;
}

[System.Serializable]
enum TerrainTypes
{
    Ground,
    LeftWall,
    RightWall,
    Roof
}

#endregion