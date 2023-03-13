using UnityEngine;
using System;
using System.Collections;

public abstract class SpecialAttacker : MonoBehaviour
{
    protected Rigidbody2D rb2d;
    protected SnailMan snailman;

    protected void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        snailman = GetComponent<SnailMan>();
    }

    public void attack(Action callback)
    {
        StartCoroutine(doAttack(callback));
    }

    public abstract IEnumerator doAttack(Action callback);
}