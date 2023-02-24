
using System.Threading;
using UnityEngine;

/*
 * This is a (maybe temporary) enemy object,
 * for testing HP management and save/load.
 */
public class Enemy : MonoBehaviour
{
    public int damagePerSecond;
    private float lastDamageTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lastDamageTime = Time.time;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        var now = Time.time;
        if (now - lastDamageTime >= 1.0)
        {
            PlayerStatus.Instance.HPManager.damageHP(damagePerSecond);
            lastDamageTime = now;
        }
    }
}
