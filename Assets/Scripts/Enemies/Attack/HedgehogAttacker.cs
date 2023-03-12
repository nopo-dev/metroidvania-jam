using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacker/Hedgehog Attacker")]
public class HedgehogAttacker : Attacker
{
    public float duration;
    public float size;
    public float cooldown;

    protected override IEnumerator doAttack(Enemy attacker, Action callback)
    {
        // animations
        attacker.standStill();
        grow(attacker);
        yield return new WaitForSeconds(duration);
        shrink(attacker);
        yield return new WaitForSeconds(cooldown);
        callback?.Invoke();
    }

    private void grow(Enemy attacker)
    {
        attacker.transform.localScale = new Vector2(size, size);
    }

    private void shrink(Enemy attacker)
    {
        attacker.transform.localScale = new Vector2(1, 1);
    }
}