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
        // animations
        attacker.chargePlayer(chargeSpeed);
        yield return new WaitForSeconds(chargeDuration);
        attacker.standStill();
        yield return new WaitForSeconds(cooldown);
        callback?.Invoke();
    }
}