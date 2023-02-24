using System;
using UnityEngine;

/*
 * This is a (maybe temporary) enemy object,
 * for testing HP management and save/load.
 */
public class Enemy : MonoBehaviour
{
    public int damagePerSecond;
    protected float lastDamageTime;
    protected Boolean triggerIn = false;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        triggerIn = true;
        PlayerStatus.Instance.HPManager.damageHP(damagePerSecond);
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
                PlayerStatus.Instance.HPManager.damageHP(damagePerSecond);
                lastDamageTime = now;
            }
        }
    }
}
