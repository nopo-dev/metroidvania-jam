using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] private InputController _input;
    [SerializeField] private Transform _healParticles;
    [SerializeField] private float _healLockout = 0;
    [SerializeField] private float _healBufferThreshold = 0.1f;

    private Animator _animator;
    private float _healTimer, _healBufferTimer, _healCooldown;
    private float _healTime;
    private bool _inHeal;
    private bool _healPress = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_animator.SetFloat("Ranged Speed", _rangedAnimationSpeed);
        _healParticles = transform.GetChild(1);
        _healTime = _healParticles.GetComponent<ParticleSystem>().main.duration;
        _healCooldown = _healTime + _healLockout;
        //_rangedCooldown = 5f / 8f / _rangedAnimationSpeed + _rangedLockout;
    }

    private void Update()
    {
        if (PauseControl.gameIsPaused) { return; }
        _healPress |= (_input.HealPress() && PlayerStatus.Instance.EnergyManager.getCurrentEnergy() >= 25
            && PlayerStatus.Instance.HPManager.getCurrentHP() <= PlayerStatus.Instance.HPManager.getMaximumHP());
    }

    private void FixedUpdate()
    {
        // track heal cooldown
        if (!_inHeal)
        {
            _healTimer = _healCooldown;
        }
        else
        {
            _healTimer -= Time.deltaTime;
        }

        // determine if healing is allowed within heal buffer threshold
        if (_healPress)
        {
            _healPress = false;
            _healBufferTimer = _healBufferThreshold;
        }
        else if (!_healPress && _healBufferThreshold > 0f)
        {
            _healBufferTimer -= Time.deltaTime;
        }

        if (_healBufferTimer > 0f)
        {
            HealSelf();
        }

        _animator.SetBool("In Heal", _inHeal);
    }

    private void HealSelf()
    {
        // check whether player is allowed to spit
        if (_healTimer > 0f && !_inHeal && _animator.GetBool("Grounded") &&
            !_animator.GetBool("Attacking") && !_animator.GetBool("Ranged Attacking"))
        {
            _healBufferTimer = 0f;
            _healTimer = _healCooldown;
            _inHeal = true;
            StartCoroutine(CoolDown());
        }
    }

    IEnumerator CoolDown()
    {
        yield return new WaitForSeconds(_healCooldown - _healLockout);
        PlayerStatus.Instance.EnergyManager.damageEnergy(25);
        _healParticles.GetComponent<ParticleSystem>().Play();
        PlayerStatus.Instance.HPManager.healHP(1);
        _inHeal = false;
    }
}
