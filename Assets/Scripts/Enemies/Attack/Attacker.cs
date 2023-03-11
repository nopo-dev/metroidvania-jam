using FSM.Abstracts;
using System.Collections;
using UnityEngine;

public abstract class Attacker : MonoBehaviour
{
    // TODO: some animation stuff here, real attacking
    // TODO: make navManager, PatrolPoints, SightSensor, Attacker abstract. Have different implementations for different enemies.

    public float range;

    protected GameObject _player;

    protected void Start()
    {
        _player = GameObject.FindWithTag("Player");
    }

    public void attack(System.Action callback)
    {
        StartCoroutine(doAttack(callback));
    }

    public bool inRange()
    {
        return Vector2.Distance(_player.transform.position, transform.position) <= range;
    }

    protected abstract IEnumerator doAttack(System.Action callback);
}
