using System;
using System.Collections;
using UnityEngine;

/*
 * This is specifically made for snail man.
 */
[CreateAssetMenu(menuName = "Attacker/Slam Attacker")]
public class SlamAttacker : Attacker
{
    public float duration;
    public float size;
    public float cooldown;

    protected override IEnumerator doAttack(Enemy attacker, Action callback)
    {
        float startTime = Time.time;
        // animations
        yield return new WaitForSeconds(cooldown);
        grow(attacker);
        yield return new WaitForSeconds(duration);
        shrink(attacker);
        yield return new WaitForSeconds(cooldown);
        (attacker as SnailMan).addThinkingTime(Time.time - startTime);
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