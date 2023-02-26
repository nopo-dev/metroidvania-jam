using System;
using UnityEngine;

/*
 * This is a (maybe temporary) enemy object,
 * for testing HP management and save/load.
 * Be careful about inheriting it.
 */
public class Enemy : CollidableArea
{
    [SerializeField] private int damagePerSecond_;
    private float lastDamageTime;
    protected Boolean triggerIn = false;

    protected override void collisionHandler(Collider2D collision)
    {
        triggerIn = true;
        PlayerStatus.Instance.HPManager.damageHP(damagePerSecond_);
        lastDamageTime = Time.time;
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        triggerIn = false;
    }

    private void Update()
    {
        if (triggerIn)
        {
            var now = Time.time;
            if (now - lastDamageTime >= 1.0)
            {
                PlayerStatus.Instance.HPManager.damageHP(damagePerSecond_);
                lastDamageTime = now;
            }
        }
    }
}
