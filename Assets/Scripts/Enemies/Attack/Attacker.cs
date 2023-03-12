using System.Collections;
using UnityEngine;
using System;

public abstract class Attacker : ScriptableObject
{
    // TODO: some animation stuff here, real attacking
    // TODO: make navManager, PatrolPoints, SightSensor, Attacker abstract. Have different implementations for different enemies.

    public float range;

    protected GameObject player;

    protected void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void attack(Enemy attacker, Action callback)
    {
        attacker.StartCoroutine(doAttack(attacker, callback));
    }

    public bool inRange(Enemy attacker)
    {
        return Vector2.Distance(attacker.transform.position, player.transform.position) <= range;
    }

    protected abstract IEnumerator doAttack(Enemy attacker, Action callback);
}
