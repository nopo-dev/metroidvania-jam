using UnityEngine;
using System.Collections;

public class MeleeAttack : PlayerAttack
{
    [SerializeField] private InputController _input;
    [SerializeField] private GameObject _meleeHitbox;

    [SerializeField] private float _meleeSpeed = 1f;
    [SerializeField] private float _meleeLockout = 0f;
    [SerializeField] private float _meleeBufferThreshold = 0.1f;

    private Animator _animator;
    private float _meleeTimer, _meleeBufferTimer, _meleeCooldown;
    [SerializeField] private float _meleeHitBoxTime = 0.25f;
    private bool _meleePress = false, _isAttacking = false;
    private bool _inMeleeAnim;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("Melee Speed", _meleeSpeed);
        _meleeCooldown = 5f / 8f / _meleeSpeed + _meleeLockout;
        upgrade = Upgrade.MeleeAttack;
    }

    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }
        _meleePress |= (_input.MeleePress() && _enabled);
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
        _animator.SetBool("In Attack Animation", _inMeleeAnim);
    }

    public override void enable(bool enabled)
    {
        base.enable(enabled);
        _animator.SetBool("Tentacled", enabled);
    }

    private void Attack()
    {
        // check whether player is allowed to melee
        if (_meleeTimer > 0f && !_isAttacking &&
            !_animator.GetBool("Ranged Attacking") && !_animator.GetBool("In Heal"))
        {
            _meleeBufferTimer = 0f;
            _meleeTimer = _meleeCooldown;
            _inMeleeAnim = true;
            _isAttacking = true;
            int num = Random.Range(1, 4);
            AudioManager.Instance.PlayDelayedSound("Melee" + num, 0.1f);
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
            Transform swooshTf = meleeHitbox.transform.GetChild(0);
            swooshTf.localPosition = new Vector3(swooshTf.localPosition.x * -1, swooshTf.localPosition.y, 1);
            swooshTf.localScale = new Vector3(swooshTf.localScale.x * -1, swooshTf.localScale.y, 1);

        }
        meleeHitbox.SetActive(true);
        yield return new WaitForSeconds(_meleeHitBoxTime);
        Destroy(meleeHitbox);

    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds((_meleeCooldown - _meleeLockout) * 0.4f);
        StartCoroutine(SpawnMeleeHitbox());
        yield return new WaitForSeconds((_meleeCooldown - _meleeLockout) * 0.6f);
        _inMeleeAnim = false;
        yield return new WaitForSeconds(_meleeLockout);
        _isAttacking = false;

    }
}
