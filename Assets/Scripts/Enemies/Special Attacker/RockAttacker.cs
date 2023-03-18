using System;
using System.Collections;
using UnityEngine;

public class RockAttacker : SpecialAttacker
{
    [SerializeField] private GameObject _rockbox;
    [SerializeField] private float _dropHeight;
    [SerializeField] private float _dropLeft;
    [SerializeField] private float _dropRight;
    [SerializeField] private float _dropStartingSpeed;

    [SerializeField] private float _startDelay;
    [SerializeField] private int _rockNumber;
    [SerializeField] private float _timeBetweenRocks;
    [SerializeField] private float _doublePoundNumber;
    [SerializeField] private float _transitionDelay;
    [SerializeField] private float _restDuration;

    private System.Random _rand;

    new protected void Awake()
    {
        base.Awake();
        _rand = new System.Random();
    }

    public override IEnumerator doAttack(Action callback)
    {
        // Pre-attack
        Debug.Log("SnailMan - Doing rock attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(_startDelay);

        // Attack
        Debug.Log("play debris sfx");
        AudioManager.Instance.PlaySound("BossDebris");
        //AudioManager.Instance.FadeIn("BossDebris", 1f);
        snailman.StartCoroutine(pound());
        snailman.StartCoroutine(dropRocks());
        yield return new WaitForSeconds(_doublePoundNumber * snailman.animationDurations["Snail Ground Pound"]);
        AudioManager.Instance.FadeOut("BossDebris", 3f);

        // Post-attack rest
        snailman.StartCoroutine(rest());
        yield return new WaitForSeconds(_restDuration);

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
        for (int i = 0; i < _rockNumber; i++)
        {
            yield return new WaitForSeconds(_timeBetweenRocks);
            dropRock();
        }
    }

    private void dropRock() 
    {
        double x = _rand.NextDouble() * (_dropRight - _dropLeft) + _dropLeft;

        GameObject projectile = Instantiate(_rockbox, new Vector2((float) x, _dropHeight), Quaternion.identity);
        projectile.SetActive(true);

        projectile.GetComponent<ProjectileBehavior>().SetAngle(Vector2.down);
        projectile.GetComponent<ProjectileBehavior>().SetSpeed(_dropStartingSpeed);
        projectile.GetComponent<ProjectileBehavior>().SetIgnoreTag(gameObject.tag);
        int num = UnityEngine.Random.Range(1,4);
        projectile.GetComponent<ProjectileBehavior>().SetImpactAudioClip("RockFall"+num);
    }

}