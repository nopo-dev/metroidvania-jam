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
        Debug.Log("SnailMan - slam attacking");
        Debug.Log("SnailMan - Pausing decision timer");
        float startTime = Time.time;
        // animations
        attacker.facePlayer();
        yield return new WaitForSeconds(cooldown);
        grow(attacker);
        yield return new WaitForSeconds(duration);
        shrink(attacker);
        yield return new WaitForSeconds(cooldown);
        float ignoredTime = Time.time - startTime;
        Debug.Log($"SnailMan - Resuming decision timer with {ignoredTime} ignored time.");
        (attacker as SnailMan).addThinkingTime(ignoredTime);
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