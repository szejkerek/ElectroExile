using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CollisionVariables _collisionVariables;

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
        PlayerDirection();
        WallInteractions();
        Walking();
        Jumping();
    }
    void GatherInputs()
    {
        _playerInputs.RawX = (int)Input.GetAxisRaw("Horizontal");
        _playerInputs.RawY = (int)Input.GetAxisRaw("Vertical");
        _playerInputs.X = Input.GetAxis("Horizontal");
        _playerInputs.Y = Input.GetAxis("Vertical");
    }

    #region Wall Interactions

    public bool IsGrounded { get; private set; }
    public event Action OnGroundTouched;

    private readonly Collider2D[] _groundCollision = new Collider2D[1];
    private readonly Collider2D[] _leftWallCollision = new Collider2D[1];
    private readonly Collider2D[] _rightWallCollision = new Collider2D[1];
    private readonly Collider2D[] _roofCollision = new Collider2D[1];
    private Vector3 _overlapOffset = new Vector3(0, 0, 0);
    bool _isPushingLeftWall, _isPushingRightWall, _isPushingRoof;

    private void CheckForTerrainCollisions()
    {
        _overlapOffset.y = _collisionVariables.groundCheckOffset;
        _isTouchingTerrain[TerrainTypes.Ground] = CheckForOverlap(_collisionVariables.groundCheckRadius, _groundCollision);

        _overlapOffset.y = _collisionVariables.roofCheckOffset;
        _isTouchingTerrain[TerrainTypes.Roof] = CheckForOverlap(_collisionVariables.roofCheckRadius,_roofCollision);

        _overlapOffset.y = 0;
        _overlapOffset.x = _collisionVariables.wallsCheckOffset;
        _isTouchingTerrain[TerrainTypes.RightWall] = CheckForOverlap(_collisionVariables.wallsCheckRadius,_groundCollision);

        _overlapOffset.x = -_collisionVariables.wallsCheckOffset;
        _isTouchingTerrain[TerrainTypes.LeftWall] = CheckForOverlap(_collisionVariables.wallsCheckRadius, _groundCollision);
        _overlapOffset.x = 0;

        bool CheckForOverlap(float checkRadius, Collider2D[] result)
        {
            return Physics2D.OverlapCircleNonAlloc(transform.position + _overlapOffset, checkRadius, result, _collisionVariables.groundMask) > 0;
        }
    }

    void WallInteractions()
    {
        CheckForTerrainCollisions();

        if(!IsGrounded && _isTouchingTerrain[TerrainTypes.Ground])
        {
            IsGrounded = true;
            _hasJumped = false;
            OnGroundTouched?.Invoke();
        }

        if (IsGrounded && !_isTouchingTerrain[TerrainTypes.Ground])
        {
            _timeLeftGrounded = Time.time;
            IsGrounded = false;
        }

        Debug.Log($"IsGrounded: {IsGrounded}   _hasJumped {_hasJumped}  _timeLeftGrounded {_timeLeftGrounded}");
        _isPushingLeftWall  = _isTouchingTerrain[TerrainTypes.LeftWall]  && _playerInputs.X < 0;
        _isPushingRightWall = _isTouchingTerrain[TerrainTypes.RightWall] && _playerInputs.X > 0;
        _isPushingRoof      = _isTouchingTerrain[TerrainTypes.Roof]      && _playerInputs.Y > 0;
    }

    void PlayerDirection()
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

    #endregion

    #region Walking
    [SerializeField] MovementVariables _movementVariables;

    void Walking()
    {
        HandleChangingDirection();
        Vector3 desiredVelocity = new Vector3(_playerInputs.X * _movementVariables.walkSpeed, _rigidbody2D.velocity.y);
        _rigidbody2D.velocity = Vector3.MoveTowards(_rigidbody2D.velocity, desiredVelocity, _movementVariables.currentMovementLerpSpeed * Time.deltaTime);
    }

    void HandleChangingDirection()
    {
        float calculatedAcceleration = IsGrounded ? _movementVariables.acceleration : _movementVariables.acceleration * 0.5f;
        if (_playerInputs.RawX == -1)
        {
            if (_rigidbody2D.velocity.x > 0) _playerInputs.X = 0; //Instant stop
            _playerInputs.X = Mathf.MoveTowards(_playerInputs.X, -1, calculatedAcceleration * Time.deltaTime);
        }
        else if(_playerInputs.RawX == 1)
        {
            if (_rigidbody2D.velocity.x < 0) _playerInputs.X = 0; //Instant stop
            _playerInputs.X = Mathf.MoveTowards(_playerInputs.X, 1, calculatedAcceleration * Time.deltaTime);
        }
        else
        {
            _playerInputs.X = Mathf.MoveTowards(_playerInputs.X, 0, calculatedAcceleration * 2 * Time.deltaTime);
        }
    }
    #endregion

    [SerializeField] JumpingVariables jumpingVariables;
    private float _timeLeftGrounded;
    private float _timeJupmed;
    private bool  _hasJumped;
    void Jumping()
    {
        //Reset jumping ability after some cooldown when player didn't leave ground check after jump 
        if (_hasJumped && _isTouchingTerrain[TerrainTypes.Ground] && Time.time > _timeJupmed + jumpingVariables.hitHeadJumpCooldown)
        {
            _hasJumped = false;
        }

        if (Input.GetKeyDown(jumpingVariables.jumpButton))
        {
            if(IsGrounded || Time.time < _timeLeftGrounded + jumpingVariables.coyoteTime)
            {
                if (!_hasJumped)
                {
                    ExecuteJump(new Vector2(_rigidbody2D.velocity.x, jumpingVariables.jumpForce));
                }
            }
        }

        if (_rigidbody2D.velocity.y < jumpingVariables.jumpVelocityFalloff || _rigidbody2D.velocity.y > 0 && !Input.GetKey(jumpingVariables.jumpButton))
        {
            _rigidbody2D.velocity += jumpingVariables.fallMultiplier * Physics.gravity.y * Vector2.up * Time.deltaTime;
        }
    }

    void ExecuteJump(Vector2 dir)
    {
        _rigidbody2D.velocity = dir;
        _timeJupmed = Time.time;
        _hasJumped = true;
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
public struct JumpingVariables
{
    public KeyCode jumpButton;
    public float hitHeadJumpCooldown;
    public float jumpForce;
    public float fallMultiplier;
    public float jumpVelocityFalloff;
    public float coyoteTime;
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

[System.Serializable]
public struct MovementVariables
{
    public float walkSpeed;
    public float acceleration;
    public float currentMovementLerpSpeed;
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