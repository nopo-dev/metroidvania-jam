using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Attacker/Chase Attacker")]
public class ChaseAttacker : Attacker
{
    [Range(0.0f, 1.0f)]
    public float chaseSpeed = 1; // speed works kinda awkwardly atm

    protected override IEnumerator doAttack(Enemy attacker, Action callback)
    {
        // TODO: fly is awk, since it's exclusive with move but not related.
        // animations
        yield return null;

        // Pre-attack
        attacker.facePlayer();

        // Attack
        attacker.chasePlayer(chaseSpeed);

        // Finish
        callback?.Invoke();
    }
}