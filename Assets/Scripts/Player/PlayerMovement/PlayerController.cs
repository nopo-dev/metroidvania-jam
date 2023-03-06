using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// TODO: push player out of colliders if they are inside one
// preferably towards the closest edge/towards player center
// TODO: move _playerActive flag to PlayerStatus perhaps?
// it currently just exists to allow Unity to smooth out before applying movement
// which solves some weird behavior with pausing and also falling through the floor
public class PlayerController : MonoBehaviour
{
    private Vector3 _velocity;

    //
    private bool _playerActive = false;
    private float _delay = 1f;
    private void Start()
    {
        StartCoroutine(Sleep());
    }

    IEnumerator Sleep()
    {
        yield return new WaitForSeconds(_delay);
        _playerActive = true;
    }

    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }

        _velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;

        if (_playerActive)
        {
            GetInput();
            CheckCollisions();

            CalculateXMove();
            CalculateFallSpeed();
            CalculateGravity();
            CalculateJump();

            ApplyMovement();
        }
    }

    private PlayerInput _input;
    private void GetInput()
    {
        _input = new PlayerInput
        {
            JumpPressed = UnityEngine.Input.GetButtonDown("Jump"),
            JumpReleased = UnityEngine.Input.GetButtonUp("Jump"),
            X = UnityEngine.Input.GetAxisRaw("Horizontal")
        };

        if (_input.JumpPressed)
        {
            _lastJumpPress = Time.time;
        }
    }

    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private Bounds _playerBounds;
    [SerializeField] private int _rayCount = 3;
    [SerializeField] private float _rayLength = 0.1f;
    [SerializeField] private float _rayBuffer = 0.1f;
    private RayRange _raysUp, _raysRight, _raysLeft, _raysDown;
    private bool _collideUp, _collideRight, _collideLeft, _collideDown;

    private void CheckCollisions()
    {
        PositionRayRanges();

        bool groundCheck = DetectCollision(_raysDown);
        if (_collideDown && !groundCheck)
        {
            _timeSinceGround = Time.time;
        }
        else if (!_collideDown && groundCheck)
        {
            _coyoteAllowed = true;
        }

        _collideDown = groundCheck;
        _collideUp = DetectCollision(_raysUp);
        _collideLeft = DetectCollision(_raysLeft);
        _collideRight = DetectCollision(_raysRight);
    }

    private void PositionRayRanges()
    { 
        Bounds bounds = new Bounds (transform.position + _playerBounds.center, _playerBounds.size);

        _raysUp = new RayRange(bounds.min.x + _rayBuffer, bounds.max.y,
                              bounds.max.x - _rayBuffer, bounds.max.y, Vector2.up);
        _raysRight = new RayRange(bounds.max.x, bounds.min.y + _rayBuffer,
                                 bounds.max.x, bounds.max.y - _rayBuffer, Vector2.right);
        _raysLeft = new RayRange(bounds.min.x, bounds.min.y + _rayBuffer,
                                bounds.min.x, bounds.max.y - _rayBuffer, Vector2.left);
        _raysDown = new RayRange(bounds.min.x + _rayBuffer, bounds.min.y,
                                bounds.max.x - _rayBuffer, bounds.min.y, Vector2.down);
    }

    private bool DetectCollision(RayRange range)
    {
        return GetRaysInRange(range).Any(
            point => Physics2D.Raycast(point, range.Direction, _rayLength, _groundLayer));
    }

    private IEnumerable<Vector2> GetRaysInRange(RayRange range)
    {
        for (int i = 0; i < _rayCount; i++)
        {
            float t = (float)i / (_rayCount - 1);
            yield return Vector2.Lerp(range.Start, range.End, t);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + _playerBounds.center, _playerBounds.size);

        if (!Application.isPlaying)
        {
            PositionRayRanges();
            Gizmos.color = Color.blue;
            foreach (RayRange range in new List<RayRange> {_raysUp, _raysRight, _raysLeft, _raysDown})
            {
                foreach (Vector2 point in GetRaysInRange(range))
                {
                    Gizmos.DrawRay(point, range.Direction * _rayLength);
                }
            }
        }
    }

    [SerializeField] private float _maxXSpeed = 15f;
    [SerializeField] private float _acceleration = 90f;
    [SerializeField] private float _deceleration = 60f;
    [SerializeField] private float _apexBoost = 2f;
    private Vector3 _lastPosition;
    private float _xSpeed;

    private void CalculateXMove()
    {
        if (_input.X != 0)
        {
            _xSpeed += _input.X * _acceleration * Time.deltaTime;
            if (_input.X > 0f)
            {
                transform.localScale = new Vector3(1f, 1f);
            }
            else if (_input.X < 0f)
            {
                transform.localScale = new Vector3(-1f, 1f);
            }
            _xSpeed = Mathf.Clamp(_xSpeed, -_maxXSpeed, _maxXSpeed);
            float apexBoost = Mathf.Sign(_input.X) * _apexBoost * _apex;
            _xSpeed += apexBoost * Time.deltaTime;
        }
        else
        {
            _xSpeed = Mathf.MoveTowards(_xSpeed, 0, _deceleration * Time.deltaTime);
        }

        if (_xSpeed > 0 && _collideRight || _xSpeed < 0 && _collideLeft)
        {
            _xSpeed = 0;
        }
    }

    [SerializeField] private float _maxFallSpeed = -40f;
    private float _ySpeed, _fallSpeed;

    private void CalculateGravity()
    {
        if (_collideDown)
        {
            if (_ySpeed < 0)
            {
                _ySpeed = 0;
            }
        }
        else
        {
            float fallSpeed = _shortJump && _ySpeed > 0 ? _fallSpeed * _shortJumpGravity : _fallSpeed;
            _ySpeed -= fallSpeed * Time.deltaTime;
            if (_ySpeed < _maxFallSpeed)
            {
                _ySpeed = _maxFallSpeed;
            }
        }
    }

    [SerializeField] private float _jumpSpeed = 35f;
    [SerializeField] private float[] _fallSpeedRange = {80f, 120f}; 
    [SerializeField] private float _apexThreshold = 10f;
    [SerializeField] private float _coyoteThreshold = 0.1f;
    [SerializeField] private float _jumpBuffer = 0.1f;
    [SerializeField] private float _shortJumpGravity = 3f;
    private float _apex, _lastJumpPress, _timeSinceGround;
    private bool _shortJump = true, _coyoteAllowed;
    private bool CanCoyote => _coyoteAllowed && !_collideDown &&
                              _timeSinceGround + _coyoteThreshold > Time.time;
    private bool JumpBuffered => _collideDown && _lastJumpPress + _jumpBuffer > Time.time;

    private void CalculateFallSpeed()
    {
        if (!_collideDown)
        {
            _apex = Mathf.InverseLerp(_apexThreshold, 0, Mathf.Abs(_velocity.y));
            _fallSpeed = Mathf.Lerp(_fallSpeedRange[0], _fallSpeedRange[1], _apex);
        }
        else
        {
            _apex = 0;
        }
    }

    private void CalculateJump()
    {
        if (_input.JumpPressed && CanCoyote || JumpBuffered)
        {
            Debug.Log("Hit!");
            CinemachineShake.Instance.ShakeCamera(8f, 0.3f);

            _ySpeed = _jumpSpeed;
            _shortJump = false;
            _coyoteAllowed = false;
            _timeSinceGround = float.MinValue;
        }

        if (!_collideDown && _input.JumpReleased && !_shortJump && _velocity.y > 0)
        {
            _shortJump = true;
        }

        if (_collideUp)
        {
            if (_ySpeed > 0)
            {
                _ySpeed = 0;
            }
        }
    }

    [SerializeField] private int _colliderIterations = 10;

    private void ApplyMovement()
    {
        Vector3 position = transform.position + _playerBounds.center;
        Vector3 movement = new Vector3(_xSpeed, _ySpeed) * Time.deltaTime;
        Vector3 endPosition = position + movement;

        Collider2D hit = Physics2D.OverlapBox(endPosition, _playerBounds.size, 0, _groundLayer);
        if (!hit)
        {
            transform.position += movement;
            return;
        }

        Vector3 adjustedEndPosition = transform.position;
        for (int i = 0; i < _colliderIterations; i++)
        {
            float t = (float)i / _colliderIterations;
            Vector3 tryPosition = Vector3.Lerp(position, endPosition, t);
            if (Physics2D.OverlapBox(tryPosition, _playerBounds.size, 0, _groundLayer))
            {
                transform.position = adjustedEndPosition;

                if (i == 1)
                {
                    if (_ySpeed < 0)
                    {
                        _ySpeed = 0;
                    }
                    Vector3 direction = transform.position - hit.transform.position;
                    transform.position += direction.normalized * movement.magnitude;
                }

                return;
            }

            adjustedEndPosition = tryPosition;
        }
    }
}