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

        AudioManager.Instance.FadeOut("BossBGM", 0.5f);
        AudioManager.Instance.PlayDelayedSound("BossDeathJingle", 1f);

        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(animationDurations["Death"] + 4f);
        // todo: run animation, make it noncollidable (physics) but body falls w/ gravity ?
        FinalMenu.Instance.show();
        foreach (Sound s in AudioManager.Instance.getSounds())
        {
            s.source.Stop();
        }
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

    public void snailAudio(string clip)
    {
        if (clip == "BossPound")
        {
            int num = UnityEngine.Random.Range(1,5);
            AudioManager.Instance.PlaySound(clip+num);
            return;
        }
        AudioManager.Instance.PlaySound(clip);
    }
}
