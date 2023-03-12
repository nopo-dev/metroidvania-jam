using System;
using System.Collections;
using UnityEngine;

public class SpitAttacker : SpecialAttacker
{
    public override IEnumerator doAttack(Action callback)
    {
        Debug.Log("Doing spit attack");
        yield return new WaitForSeconds(3);
        callback?.Invoke();
    }
}