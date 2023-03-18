using System;
using System.Collections;
using UnityEngine;

public class TraversalAttacker : SpecialAttacker
{
    // these shoudl be [SerializeField] private
    public float jumpStrength;
    public float startDelay;
    public float duration; // this is kinda dumb but don't have a way to calc flight time yet
    public float standUpDelay;
    public float endDelay;
    public bool startJumpRight;

    public GameObject traversalHitbox;

    private bool _jumpRight;

    new protected void Awake()
    {
        base.Awake();
        _jumpRight = startJumpRight;
    }

    public override IEnumerator doAttack(Action callback)
    {
        // Pre-attack
        Debug.Log("SnailMan - Beginning traversal attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(startDelay);

        // Attack - begin jump
        Debug.Log("SnailMan - Jumping");
        GameObject initialSlam = Instantiate(traversalHitbox, transform.GetChild(3).position, transform.rotation);
        snailman.animator.SetTrigger("Traversal");
        yield return new WaitForSeconds(snailman.animationDurations["Snail Traversal Slam"]);
        GameObject.Destroy(initialSlam);

        // Attack - fly through the air
        snailman.StartCoroutine(jump());
        yield return new WaitForSeconds(duration);

        // Attack - land
        Debug.Log("SnailMan - Landed");
        snailman.animator.SetTrigger("Landed");
        GameObject landingSlam = Instantiate(traversalHitbox, transform.GetChild(3).position, transform.rotation);
        yield return new WaitForSeconds(standUpDelay);
        snailman.animator.SetTrigger("Stand Up");
        GameObject.Destroy(landingSlam);

        // Post-attack
        snailman.facePlayer();
        yield return new WaitForSeconds(endDelay);

        // Finished
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