using System.Collections;
using UnityEngine;

public class HedgehogAttacker : Attacker
{
    public float duration;
    public float size;
    public float cooldown;

    private GenericEnemyController _controller;

    new private void Start()
    {
        base.Start();
        _controller = GetComponent<Move>()._input as GenericEnemyController;
    }

    protected override IEnumerator doAttack(System.Action callback)
    {
        // animations
        standStill();
        grow();
        yield return new WaitForSeconds(duration);
        shrink();
        yield return new WaitForSeconds(cooldown);
        callback?.Invoke();
    }

    private void standStill()
    {
        _controller.direction = 0;
    }

    private void grow()
    {
        transform.localScale = new Vector2(size, size);
    }

    private void shrink()
    {
        transform.localScale = new Vector2(1, 1);
    }
}