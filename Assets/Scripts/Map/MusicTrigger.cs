using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : CollidableArea
{
    protected override void collisionHandler(Collider2D other)
    {
        if (!SaveAndLoader.Instance.EnemySaveManager.bossKilled())
        {
            AudioManager.Instance.FadeOut("BackgroundTheme", 1f);
            AudioManager.Instance.PlayDelayedSound("BossBGMIntro", 1f);
            AudioManager.Instance.PlayDelayedSound("BossBGM", 1.375f);
        }
        gameObject.SetActive(false);
    }
}
