using System;
using System.Collections;
using UnityEngine;

public class RockAttacker : SpecialAttacker
{
    public override IEnumerator doAttack(Action callback)
    {
        Debug.Log("Doing rock attack");
        yield return new WaitForSeconds(3);
        callback?.Invoke();
    }
}