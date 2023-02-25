using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Enemy
{
    private void Awake()
    {
        // idk if we want to set these inspector or defined in code.
        damagePerSecond = 20;
    }

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        this.triggerIn = true;
        PlayerStatus.Instance.HPManager.damageHP(this.damagePerSecond);
        this.lastDamageTime = Time.time;
        // should modify location to last safe location
        SaveAndLoader.Instance.teleportPlayer(PlayerStatus.Instance.LastSafeLocManager.getLastSafeLoc());
        // might also want to trigger some animation here
    }

}
