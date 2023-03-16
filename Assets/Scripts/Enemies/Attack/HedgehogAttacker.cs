using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacker/Hedgehog Attacker")]
public class HedgehogAttacker : Attacker
{
    [Range(0f, 1f)]
    public float attackMoveSpeed;
    public float cooldown;

    protected override IEnumerator doAttack(Enemy attacker, Action callback)
    {
        // Pre-attack
        attacker.standStill();
        attacker.facePlayer();

        // Attack
        attacker.animator.SetBool("Attacking", true);
        // Kind of a hack to extend hitbox
        attacker.chargePlayer(attackMoveSpeed);
        yield return new WaitForSeconds(attacker.animationDurations["Attack"]);
        // Probably we want to extend a new hitbox or even own collider here.
        attacker.standStill();

        // Post-attack
        attacker.animator.SetBool("Attacking", false);
        yield return new WaitForSeconds(cooldown); // This is not necessary if already part of animation.

        // Finish
        callback?.Invoke();
    }
}