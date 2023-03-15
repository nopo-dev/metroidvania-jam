using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] public InputController _input = null; // making this public is kind of a hack for enemy movement
    // 8 ppu values
    [SerializeField] private float _maxSpeed = 16f;
    // snappier movement
    [SerializeField] private float _maxAcceleration = 250f;
    [SerializeField] private float _maxAerialAcceleration = 250f;

    private Vector2 _direction;
    private Vector2 _targetVelocity;
    private Vector2 _velocity;
    private Rigidbody2D _rb;
    private CollisionSurface _ground;
    private Animator _animator;

    private float _maxSpeedChange;
    private float _acceleration;
    private bool _grounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ground = GetComponent<CollisionSurface>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }

        _direction.x = _input.MoveInput();
        // determine how fast we're allowed to move
        _targetVelocity = new Vector2(_direction.x, 0f) * Mathf.Max(_maxSpeed - _ground.Friction, 0f);
    }

    private void FixedUpdate()
    {
        // apply movement up to max allowed acceleration
        _grounded = _ground.Grounded;
        _velocity = _rb.velocity;
        _acceleration = _grounded ? _maxAcceleration : _maxAerialAcceleration;
        _maxSpeedChange = _acceleration * Time.deltaTime;
        _velocity.x = Mathf.MoveTowards(_velocity.x, _targetVelocity.x, _maxSpeedChange);

        _rb.velocity = _velocity;
        _animator.SetFloat("Velocity X", _velocity.x);
        if (_velocity.x > 0)    transform.localScale = new Vector3(1, 1, 1);
        else if (_velocity.x < 0)   transform.localScale = new Vector3(-1, 1, 1);
    }

}
