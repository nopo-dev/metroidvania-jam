using System;
using System.Collections;
using UnityEngine;

public class SnailMan : Enemy
{
    [HideInInspector] public bool deciding = false;
    [HideInInspector] public static int randomAttackDecider; // TODO: this is dumb hack

    private TraversalAttacker _traversalAttacker;
    private SpitAttacker _spitAttacker;
    private RockAttacker _rockAttacker;
    private System.Random _rand = new System.Random();

    new protected void Awake()
    {
        base.Awake();
        _respawns = false;
        _traversalAttacker = GetComponent<TraversalAttacker>();
        _spitAttacker = GetComponent<SpitAttacker>();
        _rockAttacker = GetComponent<RockAttacker>();
        randomAttackDecider = -Consts.NUM_SNAIL_SPECIAL_ATTACKS;
    }

    public void nextRandomAttack()
    {
        if (randomAttackDecider < 0)
        {
            randomAttackDecider++;
        }
        else
        {
            randomAttackDecider = _rand.Next(Consts.NUM_SNAIL_SPECIAL_ATTACKS);
        }
    }

    protected override IEnumerator dyingAnimation(Action callback)
    {
        var rb2d = GetComponent<Rigidbody2D>();
        rb2d.bodyType = RigidbodyType2D.Static;
        rb2d.velocity = new Vector2(0, 0); // not sure if needed after static

        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(animationDurations["Death"]);
        // todo: run animation, make it noncollidable (physics) but body falls w/ gravity ?

        callback?.Invoke();
    }

    public bool readyToAttack()
    {
        return deciding && (_navManager as Decider).hasDecided();
    }

    public void addThinkingTime(float time)
    {
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
