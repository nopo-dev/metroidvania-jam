using UnityEngine;
using System.Collections;

public class RangedAttack : MonoBehaviour
{
    [SerializeField] private InputController _input;
    [SerializeField] private GameObject _rangedHitbox;

    [SerializeField] private float _rangedAnimationSpeed = 1f;
    [SerializeField] private float _projectileSpeed = 1f;
    [SerializeField] private Vector3 _projectileAngle = new Vector3(1f, 1f, 0f);
    [SerializeField] private float _rangedLockout = 0f;
    [SerializeField] private float _rangedBufferThreshold = 0.1f;

    private Animator _animator;
    private float _rangedTimer, _rangedBufferTimer, _rangedCooldown;
    private bool _rangedPress = false, _isRangedAttacking = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Ranged Speed", _rangedAnimationSpeed);
        _rangedCooldown = 5f / 8f / _rangedAnimationSpeed + _rangedLockout;
    }

    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }
        _rangedPress |= (_input.RangedPress() && _animator.GetBool("Spittable"));
    }

    private void FixedUpdate()
    {
        // track ranged cooldown
        if (!_isRangedAttacking)
        {
            _rangedTimer = _rangedCooldown;
        }
        else
        {
            _rangedTimer -= Time.deltaTime;
        }

        // determine if spitting is allowed within ranged buffer threshold
        if (_rangedPress)
        {
            _rangedPress = false;
            _rangedBufferTimer = _rangedBufferThreshold;
        }
        else if (!_rangedPress && _rangedBufferThreshold > 0f)
        {
            _rangedBufferTimer -= Time.deltaTime;
        }

        if (_rangedBufferTimer > 0f)
        {
            AttackRanged();
        }

        _animator.SetBool("Ranged Attacking", _isRangedAttacking);
    }

    private void AttackRanged()
    {
        // check whether player is allowed to spit
        if (_rangedTimer > 0f && !_isRangedAttacking &&
            !_animator.GetBool("Attacking"))
        {
            _rangedBufferTimer = 0f;
            _rangedTimer = _rangedCooldown;
            _isRangedAttacking = true;
            SpawnProjectile();
            StartCoroutine(CoolDown());
        }
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(_rangedHitbox,
            transform.GetChild(0).position, transform.rotation);
        projectile.SetActive(true);
        Vector3 angle = _projectileAngle;
        angle.x *= transform.localScale.x;
        _projectileAngle = angle;
        projectile.GetComponent<ProjectileBehavior>().SetAngle(_projectileAngle);
        projectile.GetComponent<ProjectileBehavior>().SetSpeed(_projectileSpeed);
        projectile.GetComponent<ProjectileBehavior>().SetIgnoreTag(gameObject.tag);
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_rangedCooldown);
        _isRangedAttacking = false;
    }
}
