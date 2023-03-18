using System;
using System.Collections;
using UnityEngine;

public class SpitAttacker : SpecialAttacker
{
    [SerializeField] private GameObject _spitbox;
    [SerializeField] private float _rangedAnimationSpeed = 1f;
    [SerializeField] private float _projectileSpeed = 1f;
    [SerializeField] private Vector3 _projectileAngle = new Vector3(1f, 1f, 0f);

    [SerializeField] private float _startDelay;
    [SerializeField] private int _spitNumber;
    [SerializeField] private float _spitDuration;
    [SerializeField] private float _timeBetweenSpits;
    [SerializeField] private float _spitHeightThreshold;
    [SerializeField] private float _endDelay;

    public override IEnumerator doAttack(Action callback)
    {
        // Pre-attack
        Debug.Log("SnailMan - Doing spit attack");
        snailman.facePlayer();
        yield return new WaitForSeconds(_startDelay);

        // Charging
        snailman.animator.SetTrigger("Spit Charge");
        yield return new WaitForSeconds(snailman.animationDurations["Snail Spit Charge"]);

        // Attack
        Debug.Log("SnailMan - Spitting");
        snailman.StartCoroutine(spit());
        // Making the assumption that spit high and spit low have same clip length
        yield return new WaitForSeconds((_timeBetweenSpits + _spitDuration) * _spitNumber);

        // Post-attack
        Debug.Log("SnailMan - Done spitting");
        snailman.animator.SetTrigger("Spit Done");
        yield return new WaitForSeconds(_endDelay);
        
        // Finish
        callback?.Invoke();
    }
    
    private IEnumerator spit()
    {
        for (int i = 0; i < _spitNumber; i++)
        {
            if (snailman.player.transform.position.y - transform.position.y > _spitHeightThreshold)
            {
                Debug.Log("SnailMan - Spitting high");
                snailman.animator.SetTrigger("Spit High");
                doSpit(true);
            }
            else
            {
                Debug.Log("SnailMan - Spitting Low");
                snailman.animator.SetTrigger("Spit Low");
                doSpit(false);
            }
            yield return new WaitForSeconds(_spitDuration);
            snailman.animator.SetTrigger("Spat");
            yield return new WaitForSeconds(_timeBetweenSpits);
        }
    }

    private void doSpit(bool high) 
    {
        GameObject projectile = Instantiate(_spitbox, 
            high ? transform.GetChild(0).position : transform.GetChild(1).position, 
            transform.rotation);
        projectile.SetActive(true);
        Vector3 angle = _projectileAngle;
        angle.x *= transform.localScale.x;

        projectile.GetComponent<ProjectileBehavior>().SetAngle(angle);
        projectile.GetComponent<ProjectileBehavior>().SetSpeed(_projectileSpeed);
        projectile.GetComponent<ProjectileBehavior>().SetIgnoreTag(gameObject.tag);

        AudioManager.Instance.PlaySound("PlayerSpit");
    }

}