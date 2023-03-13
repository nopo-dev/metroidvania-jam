using System;
using System.Collections;
using UnityEngine;

public class SpitAttacker : SpecialAttacker
{
    public float startDelay;
    public int spitNumber;
    public float durationPerSpit;
    public float endDelay;

    public override IEnumerator doAttack(Action callback)
    {
        Debug.Log("SnailMan - Doing spit attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(startDelay);
        Debug.Log("SnailMan - Spitting");
        snailman.StartCoroutine(spit());
        yield return new WaitForSeconds(durationPerSpit * spitNumber);
        Debug.Log("SnailMan - Done spitting");
        yield return new WaitForSeconds(endDelay);
        callback?.Invoke();
    }
    
    private IEnumerator spit()
    {
        for (int i = 0; i < spitNumber; i++)
        {
            rb2d.velocity = new Vector2(-2f, 0);
            yield return new WaitForSeconds(durationPerSpit / 2);
            rb2d.velocity = new Vector2(2f, 0);
            yield return new WaitForSeconds(durationPerSpit / 2);
        }
        rb2d.velocity = new Vector2(0, 0);
    }
}