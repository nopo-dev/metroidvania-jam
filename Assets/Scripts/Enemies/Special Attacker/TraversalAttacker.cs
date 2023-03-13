using System;
using System.Collections;
using UnityEngine;

public class TraversalAttacker : SpecialAttacker
{
    public float jumpStrength;
    public float startDelay;
    public float duration;
    public float endDelay;
    public bool startJumpRight;

    private bool _jumpRight;

    new protected void Awake()
    {
        base.Awake();
        _jumpRight = startJumpRight;
    }

    public override IEnumerator doAttack(Action callback)
    {
        Debug.Log("SnailMan - Beginning traversal attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(startDelay);
        Debug.Log("SnailMan - Jumping");
        snailman.StartCoroutine(jump());
        yield return new WaitForSeconds(duration);
        Debug.Log("SnailMan - Landed");
        snailman.facePlayer();
        yield return new WaitForSeconds(endDelay);

        callback?.Invoke();
    }

    private IEnumerator jump()
    {
        rb2d.velocity = new Vector2((_jumpRight ? jumpStrength : -jumpStrength), jumpStrength);
        yield return new WaitForSeconds(duration);
        _jumpRight = !_jumpRight;
        rb2d.velocity = new Vector2(0, 0);
    }
}