using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacker/Charge Attacker")]
public class ChargeAttacker : Attacker
{
    public float chargeDuration;
    public float cooldown;
    [Range(0.0f, 1.0f)]
    public float chargeSpeed = 1;

    protected override IEnumerator doAttack(Enemy attacker, Action callback)
    {
        // Attack
        attacker.animator.SetBool("Attacking", true);
        attacker.chargePlayer(chargeSpeed);
        // not sure if this should be tied to animation length actually
        yield return new WaitForSeconds(attacker.animationDurations["Attack"]);

        // Post-attack
        attacker.animator.SetBool("Attacking", false);
        attacker.standStill();
        yield return new WaitForSeconds(cooldown);
        
        // Finish
        callback?.Invoke();
    }
}