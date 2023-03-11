using FSM.Abstracts;
using System.Collections;
using UnityEngine;

public class ChargeAttacker : Attacker
{
    public float chargeDuration;
    public float cooldown;
    [Range(0.0f, 1.0f)]
    public float chargeSpeed = 1;

    private GenericEnemyController _controller;

    new private void Start()
    {
        base.Start();
        _controller = GetComponent<Move>()._input as GenericEnemyController;
    }

    protected override IEnumerator doAttack(System.Action callback)
    {
        // animations
        _controller.direction = (transform.position.x > _player.transform.position.x) ? -chargeSpeed : chargeSpeed;
        yield return new WaitForSeconds(chargeDuration);
        _controller.direction = 0;
        yield return new WaitForSeconds(cooldown);
        callback?.Invoke();
    }
}