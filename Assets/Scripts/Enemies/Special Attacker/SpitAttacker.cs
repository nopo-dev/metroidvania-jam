using System;
using System.Collections;
using UnityEngine;

public class SpitAttacker : SpecialAttacker
{
    public float startDelay;
    public int spitNumber;
    public float spitDuration;
    public float timeBetweenSpits;
    public float spitHeightThreshold;
    public float endDelay;

    public override IEnumerator doAttack(Action callback)
    {
        // Pre-attack
        Debug.Log("SnailMan - Doing spit attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(startDelay);

        // Charging
        snailman.animator.SetTrigger("Spit Charge");
        yield return new WaitForSeconds(snailman.animationDurations["Snail Spit Charge"]);

        // Attack
        Debug.Log("SnailMan - Spitting");
        snailman.StartCoroutine(spit());
        // Making the assumption that spit high and spit low have same clip length
        yield return new WaitForSeconds((timeBetweenSpits + spitDuration) * spitNumber);

        // Post-attack
        Debug.Log("SnailMan - Done spitting");
        snailman.animator.SetTrigger("Spit Done");
        yield return new WaitForSeconds(endDelay);
        
        // Finish
        callback?.Invoke();
    }
    
    private IEnumerator spit()
    {
        for (int i = 0; i < spitNumber; i++)
        {
            if (snailman.player.transform.position.y - transform.position.y > spitHeightThreshold)
            {
                Debug.Log("SnailMan - Spitting high");
                snailman.animator.SetTrigger("Spit High");
                doSpitHigh();
            }
            else
            {
                Debug.Log("SnailMan - Spitting Low");
                snailman.animator.SetTrigger("Spit Low");
                doSpitLow();
            }
            yield return new WaitForSeconds(spitDuration);
            snailman.animator.SetTrigger("Spat");
            yield return new WaitForSeconds(timeBetweenSpits);
        }
    }

    private void doSpitHigh() { }

    private void doSpitLow() { }
}