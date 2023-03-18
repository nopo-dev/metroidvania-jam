using System;
using System.Collections;
using UnityEngine;

/*
 * This is specifically made for snail man.
 */
[CreateAssetMenu(menuName = "Attacker/Slam Attacker")]
public class SlamAttacker : Attacker
{
    public float paddingTime;
    public GameObject meleeHitbox;

    private System.Random _rand = new System.Random();

    protected override IEnumerator doAttack(Enemy attacker, Action callback)
    {
        // Pre-attack
        Debug.Log("SnailMan - slam attacking");
        Debug.Log("SnailMan - Pausing decision timer");
        float startTime = Time.time;
        // animations
        attacker.facePlayer();
        yield return new WaitForSeconds(paddingTime);

        // Attack
        int attackNumber = _rand.NextDouble() > 0.5 ? 1 : 2;
        attacker.animator.SetTrigger($"Basic Attack {attackNumber}");
        yield return new WaitForSeconds(attacker.animationDurations[$"Snail Basic Swing {attackNumber}"] * 0.66f);
        GameObject hitbox = Instantiate(meleeHitbox, attacker.transform.GetChild(2).position,
            attacker.transform.rotation);
        yield return new WaitForSeconds(attacker.animationDurations[$"Snail Basic Swing {attackNumber}"] * 0.33f);
        GameObject.Destroy(hitbox);

        // Post-attack
        // Currently, Snail will idle during padding time. We can add a trigger
        // if we want to freeze last attack frame.
        yield return new WaitForSeconds(paddingTime);
        float ignoredTime = Time.time - startTime;
        Debug.Log($"SnailMan - Resuming decision timer with {ignoredTime} ignored time.");
        (attacker as SnailMan).addThinkingTime(ignoredTime);

        // Finish
        callback?.Invoke();
    }
}