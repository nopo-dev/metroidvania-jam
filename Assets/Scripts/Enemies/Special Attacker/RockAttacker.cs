using System;
using System.Collections;
using UnityEngine;

public class RockAttacker : SpecialAttacker
{
    public float startDelay;
    public int rockNumber;
    public float timeBetweenRocks;
    public float doublePoundNumber;
    public float transitionDelay;
    public float restDuration;
    
    public override IEnumerator doAttack(Action callback)
    {
        // Pre-attack
        Debug.Log("SnailMan - Doing rock attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(startDelay);

        // Attack
        snailman.StartCoroutine(pound());
        snailman.StartCoroutine(dropRocks());
        yield return new WaitForSeconds(doublePoundNumber * snailman.animationDurations["Snail Ground Pound"]);

        // Post-attack rest
        snailman.StartCoroutine(rest());
        yield return new WaitForSeconds(restDuration);

        // Post-rest
        snailman.animator.SetTrigger("Recovered");
        
        // Finished
        callback?.Invoke();
    }

    private IEnumerator rest()
    {
        Debug.Log("SnailMan - Tired");
        snailman.animator.SetTrigger("Tired");
        yield return null;
    }

    private IEnumerator pound()
    {
        Debug.Log("SnailMan - Pounding");
        snailman.animator.SetTrigger("Ground Pound");
        yield return null;
    }
    
    private IEnumerator dropRocks()
    {
        Debug.Log("SnailMan - Dropping rocks");
        for (int i = 0; i < rockNumber; i++)
        {
            yield return new WaitForSeconds(timeBetweenRocks);
            dropRock();
        }
    }

    private void dropRock() { }
}