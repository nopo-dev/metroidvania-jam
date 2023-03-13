using System;
using System.Collections;
using UnityEngine;

public class RockAttacker : SpecialAttacker
{
    public float startDelay;
    public int rockNumber;
    public float durationPerRock;
    public float poundNumber;
    public float durationPerPound;
    public float transitionDelay;
    public float restDuration;

    public override IEnumerator doAttack(Action callback)
    {
        Debug.Log("SnailMan - Doing rock attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(startDelay);

        snailman.StartCoroutine(pound());
        snailman.StartCoroutine(dropRocks());
        yield return new WaitForSeconds(poundNumber * durationPerPound);
        rb2d.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(transitionDelay);
        snailman.StartCoroutine(rest());
        yield return new WaitForSeconds(restDuration);
        
        callback?.Invoke();
    }

    private IEnumerator rest()
    {
        Debug.Log("SnailMan - Tired");
        // play rest animation;
        yield return null;
    }

    private IEnumerator pound()
    {
        Debug.Log("SnailMan - Pounding");
        for (int i = 0; i < poundNumber; i++)
        {
            rb2d.velocity = new Vector2(0, 5);
            yield return new WaitForSeconds(durationPerPound / 2);
            rb2d.velocity = new Vector2(0, -3);
            yield return new WaitForSeconds(durationPerPound / 2);

        }
    }
    
    private IEnumerator dropRocks()
    {
        Debug.Log("SnailMan - Dropping rocks");
        for (int i = 0; i < rockNumber; i++)
        {
            yield return new WaitForSeconds(durationPerRock);
            dropRock();
        }
    }

    private void dropRock() { }
}