using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private InputController _input = null;
    // 8 ppu values
    [SerializeField] private float _jumpHeight = 160f;
    [SerializeField] private int _maxAirJumps = 0;
    // normal fall speed
    [SerializeField] private float _fallGravity = 20f;
    [SerializeField] private float _jumpGravity = 15f;
    // snappier short jump
    [SerializeField] private float _shortJumpGravity = 40f;
    [SerializeField] private float _coyoteThreshold = 0.1f;
    [SerializeField] private float _jumpBufferThreshold = 0.1f;

    private Rigidbody2D _rb;
    private Animator _animator;
    // for friction purposes, though every surface should be frictionless
    private CollisionSurface _ground;
    private Vector2 _velocity;
    private int _jumpCount;
    private float _defaultGravity, _jumpSpeed, _coyoteTimer, _jumpBufferTimer;
    private bool _jumpPress, _grounded, _isJumping;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _ground = GetComponent<CollisionSurface>();
        _defaultGravity = 1f;
    }

    private void Update()
    {
        _jumpPress |= _input.JumpPress();
    }

    private void FixedUpdate()
    {
        _grounded = _ground.Grounded;
        _velocity = _rb.velocity;

        // some rigidbody fuckery with floats
        // maybe can delete everything but _grounded here
        if (_grounded && _rb.velocity.y > -0.01f && _rb.velocity.y < 0.01f)
        {
            _jumpCount = 0;
            _coyoteTimer = _coyoteThreshold;
            _isJumping = false;
            // i hate rigidbodies
            _velocity.y = 0f;
        }
        else
        {
            _coyoteTimer -= Time.deltaTime;
        }

        // determine if jumping is allowed within jump buffer threshold
        if (_jumpPress)
        {
            _jumpPress = false;
            _jumpBufferTimer = _jumpBufferThreshold;
        }
        else if (!_jumpPress && _jumpBufferTimer > 0f)
        {
            _jumpBufferTimer -= Time.deltaTime;
        }

        if (_jumpBufferTimer > 0f)
        {
            ApplyJump();
        }

        // modify gravity scale for duration of jump
        if (_input.JumpHold() && _velocity.y > 0)
        {
            _rb.gravityScale = _jumpGravity;
        }
        else if (_velocity.y < 0)
        {
            _rb.gravityScale = _fallGravity;
        }
        // variable jump height
        else if (!_input.JumpHold() && _velocity.y >= 0)
        {
            _rb.gravityScale = _shortJumpGravity;
        }
        else
        {
            _rb.gravityScale = _defaultGravity;
        }
        _animator.SetBool("Grounded", _grounded);
        _animator.SetFloat("Velocity Y", _velocity.y);
        _rb.velocity = _velocity;
    }

    private void ApplyJump()
    {
        // check whether player is allowed to jump
        if (_coyoteTimer > 0f || (_jumpCount < _maxAirJumps && _isJumping))
        {
            if (_isJumping)
            {
                _jumpCount++;
            }

            _jumpBufferTimer = 0f;
            _coyoteTimer = 0f;
            _isJumping = true;
            // some formula i stole
            _jumpSpeed = Mathf.Sqrt(-2f * Physics.gravity.y * _jumpHeight);

            if (_velocity.y > 0f)
            {
                _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            }
            _velocity.y = _jumpSpeed;
        }
    }
}