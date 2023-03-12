using System;
using System.Collections;
using UnityEngine;

public class SnailMan : Enemy
{
    [SerializeField] public Animator animator;
    [HideInInspector] new protected bool _respawns = false;
    [HideInInspector] public bool deciding = false;

    private TraversalAttacker _traversalAttacker;
    private SpitAttacker _spitAttacker;
    private RockAttacker _rockAttacker;

    protected void Awake()
    {
        base.Awake();
        _traversalAttacker = GetComponent<TraversalAttacker>();
        _spitAttacker = GetComponent<SpitAttacker>();
        _rockAttacker = GetComponent<RockAttacker>();
    }

    protected override IEnumerator dyingAnimation(Action callback)
    {
        // play animation
        yield return new WaitForSeconds(4);
        callback?.Invoke();
    }

    public bool readyToAttack()
    {
        return deciding && (_navManager as Decider).hasDecided();
    }

    public void addThinkingTime(float time)
    {
        Debug.Log("Adding thinking time");
        (_navManager as Decider).uncountedTime += time;
    }

    public void specialAttack(SnailAttack attack, Action callback)
    {
        switch (attack)
        {
            case SnailAttack.Traversal:
                _traversalAttacker.attack(callback);
                break;
            case SnailAttack.Spit:
                _spitAttacker.attack(callback);
                break;
            case SnailAttack.Rocks:
                _rockAttacker.attack(callback);
                break;
            default:
                Debug.Log("SnailMan - Invalid attack option");
                break;
        }
    }

    public enum SnailAttack
    {
        Traversal,
        Spit,
        Rocks
    }
}
