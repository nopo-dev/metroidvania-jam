using System;
using System.Collections;
using UnityEngine;

public class SnailMan : Enemy
{
    [HideInInspector]
    new private bool _respawns = false; // todo: override correct ?
    
    public bool deciding { get; set; }

    protected override IEnumerator dyingAnimation(Action callback)
    {
        // play animation
        yield return new WaitForSeconds(4);
        callback?.Invoke();
    }

    
}
