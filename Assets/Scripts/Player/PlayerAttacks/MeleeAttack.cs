using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private InputController _input;
    [SerializeField] private GameObject _meleeHitbox;

    [SerializeField] private float _meleeSpeed = 1f;
    [SerializeField] private float _meleeLockout = 0f;
    [SerializeField] private float _meleeBufferThreshold = 0.1f;

    private Animator _animator;
    private float _meleeTimer, _meleeBufferTimer, _meleeCooldown;
    private float _meleeHitBoxTime;
    private bool _meleePress = false, _isAttacking = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Melee Speed", _meleeSpeed);
        _meleeCooldown = 5f / 8f / _meleeSpeed + _meleeLockout;
        _meleeHitBoxTime = _meleeCooldown * _meleeSpeed * 0.8f;
    }

    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }
        _meleePress |= (_input.MeleePress() && _animator.GetBool("Tentacled"));
    }

    private void FixedUpdate()
    {
        // track melee cooldown
        if (!_isAttacking)
        {
            _meleeTimer = _meleeCooldown;
        }
        else
        {
            _meleeTimer -= Time.deltaTime;
        }

        // determine if meleeing is allowed within melee buffer threshold
        if (_meleePress)
        {
            _meleePress = false;
            _meleeBufferTimer = _meleeBufferThreshold;
        }
        else if (!_meleePress && _meleeBufferThreshold > 0f)
        {
            _meleeBufferTimer -= Time.deltaTime;
        }

        if (_meleeBufferTimer > 0f)
        {
            Attack();
        }

        _animator.SetBool("Attacking", _isAttacking);
    }

    private void Attack()
    {
        // check whether player is allowed to melee
        if (_meleeTimer > 0f && !_isAttacking &&
            !_animator.GetBool("Ranged Attacking"))
        {
            _meleeBufferTimer = 0f;
            _meleeTimer = _meleeCooldown;
            _isAttacking = true;
            StartCoroutine(SpawnMeleeHitbox());
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator SpawnMeleeHitbox()
    {
        GameObject meleeHitbox = Instantiate(_meleeHitbox, transform.position, transform.rotation);
        if (transform.localScale.x == -1)
        {
            BoxCollider2D hitbox = meleeHitbox.GetComponent<BoxCollider2D>();
            Vector2 offset = hitbox.offset;
            offset.x = offset.x * -1;
            hitbox.offset = offset;
        }
        meleeHitbox.SetActive(true);
        yield return new WaitForSeconds(_meleeHitBoxTime);
        Destroy(meleeHitbox);

    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_meleeCooldown);
        _isAttacking = false;
    }
}
