using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInteraction : MonoBehaviour
{
    [SerializeField] private InputController _input;
    // 8 ppu values
    [Header("Wall Slide")]
    [SerializeField] private float _wallSlideMaxSpeed = 10f;

    [Header("Wall Jump")]
    [SerializeField] private Vector2 _wallJumpClose = new Vector2(5f, 10f);
    [SerializeField] private Vector2 _wallJumpAway = new Vector2(10f, 10f);
    [SerializeField] private float _coyoteThreshold = 0.2f;

    public bool IsWallJumping { get; private set; }

    private CollisionSurface _collisionSurface;
    private Rigidbody2D _rb;

    private Vector2 _velocity;
    private bool _walled, _grounded, _jumpPress;
    private float _wallDirection, _coyoteTimer, _jumpBufferTimer;

    private void Awake()
    {
        _collisionSurface = GetComponent<CollisionSurface>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }
        
        if (_coyoteTimer > 0f && !_grounded)
        {
            _jumpPress |= _input.JumpPress();
        }
    }

    private void FixedUpdate()
    {
        _velocity = _rb.velocity;
        _grounded = _collisionSurface.Grounded;
        _walled = _collisionSurface.Walled;
        _wallDirection = _collisionSurface.Normal.x;

        // some forgiveness for pressing the movement key early
        if (_walled)
        {
            _coyoteTimer = _coyoteThreshold;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }

        // wall slide
        // if (_walled && _input.MoveInput() != 0f)
        if (_walled && -Mathf.Sign(_wallDirection) == Mathf.Sign(_input.MoveInput()))
        {
            if (_velocity.y < -_wallSlideMaxSpeed)
            {
                _velocity.y = -_wallSlideMaxSpeed;
            }
        }

        // wall jump
        if (_jumpPress)
        {
            _jumpPress = false;
            ApplyWallJump();
        }

        _rb.velocity = _velocity;
    }

    private void ApplyWallJump()
    {
        if (_coyoteTimer > 0f && _velocity.x == 0 || _grounded)
        {
            IsWallJumping = false;
        }

        // wall jump
        if (_coyoteTimer > 0f && -Mathf.Sign(_wallDirection) == Mathf.Sign(_input.MoveInput()))
        {
            _velocity = new Vector2(_wallJumpClose.x * _wallDirection, _wallJumpClose.y);
            IsWallJumping = true;
        }
        else if (_coyoteTimer > 0f && -Mathf.Sign(_wallDirection) != Mathf.Sign(_input.MoveInput()))
        {
            _velocity = new Vector2(_wallJumpAway.x * _wallDirection, _wallJumpAway.y);
            IsWallJumping = true;
        }
    }
}
