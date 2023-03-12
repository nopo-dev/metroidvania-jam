using UnityEngine;
using System;
using System.Collections;

public abstract class SpecialAttacker : MonoBehaviour
{
    public void attack(Action callback)
    {
        StartCoroutine(doAttack(callback));
    }

    public abstract IEnumerator doAttack(Action callback);
}