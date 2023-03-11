using System.Collections;
using UnityEngine;

public class ChaseAttacker : Attacker
{
    [Range(0.0f, 1.0f)]
    public float chaseSpeed = 1;

    private Fly _flyer;

    new private void Start()
    {
        base.Start();
        _flyer = GetComponent<Fly>();
    }

    protected override IEnumerator doAttack()
    {
        // animations
        _attacking = true;
        yield return null;
        _flyer.setDirection(chaseSpeed * (_player.transform.position - transform.position).normalized);
        transform.rotation = Quaternion.LookRotation(_player.transform.position);
        _attacking = false;
    }
}