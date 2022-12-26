using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private PlayerInputs _playerInputs;
    private bool _facingLeft;
    Dictionary<TerrainTypes, bool> _isTouchingTerrain = new Dictionary<TerrainTypes, bool>();

    [Header("Detection")]
    [SerializeField] private CollisionVariables _collisionVariables;

    public bool IsGrounded { get => _isTouchingTerrain[TerrainTypes.Ground]; }

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _facingLeft = false;
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
        _isTouchingTerrain[TerrainTypes.Ground] = Physics2D.OverlapCircleNonAlloc(transform.position + overlapOffset,
                                                                                  _collisionVariables.groundCheckRadius,
                                                                                  _groundCollision,
                                                                                  _collisionVariables.groundMask) > 0;

        overlapOffset.y = _collisionVariables.roofCheckOffset;
        _isTouchingTerrain[TerrainTypes.Roof] = Physics2D.OverlapCircleNonAlloc(transform.position + overlapOffset,
                                                                                  _collisionVariables.roofCheckRadius,
                                                                                  _roofCollision,
                                                                                  _collisionVariables.groundMask) > 0;

        overlapOffset.y = 0;
        overlapOffset.x = _collisionVariables.wallsCheckOffset;
        _isTouchingTerrain[TerrainTypes.RightWall] = Physics2D.OverlapCircleNonAlloc(transform.position + overlapOffset,
                                                                                  _collisionVariables.wallsCheckRadius,
                                                                                  _groundCollision,
                                                                                  _collisionVariables.groundMask) > 0;

        overlapOffset.x = -_collisionVariables.wallsCheckOffset;
        _isTouchingTerrain[TerrainTypes.LeftWall] = Physics2D.OverlapCircleNonAlloc(transform.position + overlapOffset,
                                                                                  _collisionVariables.wallsCheckRadius,
                                                                                  _groundCollision,
                                                                                  _collisionVariables.groundMask) > 0;
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

    struct PlayerInputs
    {
        public float X, Y;
        public int RawX, RawY;
    }

    enum TerrainTypes
    {
        Ground,
        LeftWall,
        RightWall,
        Roof
    }
}

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
