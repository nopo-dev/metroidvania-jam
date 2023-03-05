using UnityEngine;
using System.Collections;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private InputController _input;

    [SerializeField] private float _meleeSpeed = 0.5f;
    [SerializeField] private float _meleeLockout = 0f;
    [SerializeField] private float _meleeBufferThreshold = 0.1f;

    private Animator _animator;
    private float _meleeTimer, _meleeBufferTimer, _meleeCooldown;
    private bool _meleePress = false, _isAttacking = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Melee Speed", _meleeSpeed);
        _meleeCooldown = 5f / 8f / _meleeSpeed + _meleeLockout;
    }

    private void Update()
    {
        _meleePress |= _input.MeleePress();

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
        if (_meleeTimer > 0f && !_isAttacking)
        {
            _meleeBufferTimer = 0f;
            _meleeTimer = _meleeCooldown;
            _isAttacking = true;
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_meleeCooldown);
        _isAttacking = false;
        Debug.Log("attack done");
    }
}
