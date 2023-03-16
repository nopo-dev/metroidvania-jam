using UnityEngine;

// TODO: a little awkward that peaceul nav lives in NavManager and attack nav lives in Attacker
public abstract class NavManager : ScriptableObject
{
    public bool flying = false;

    public abstract void doPeacefulNav(Enemy navigator);

    public void moveTowards(Enemy navigator, Vector3 target, float speed=1) // Vector2->3 implicit conversion is awkward
    {
        // Mover and flyer will handle animation
        if (flying)
        {
            navigator.flyer.setDirection(speed * (target - navigator.transform.position).normalized);
        }
        else
        {
            navigator.mover.setDirection((navigator.transform.position.x > target.x) ? -speed : speed);
        }
    }

    public virtual void startNav(Enemy navigator, Vector2 direction) { }

    public void standStill(Enemy navigator)
    {
        // Mover and flyer will handle animation
        if (flying)
        {
            // TODO : gradual slowdown ?
            navigator.flyer.setDirection(new Vector2(0, 0));
        }
        else
        {
            navigator.mover.setDirection(0);
        }
    }
    public void chargePlayer(Enemy navigator, float chargeSpeed)
    {
        moveTowards(navigator, navigator.player.transform.position, chargeSpeed);
    }

    public void chasePlayer(Enemy navigator, float chaseSpeed)
    {
        moveTowards(navigator, navigator.player.transform.position, chaseSpeed);
    }

    public void facePlayer(Enemy navigator)
    {
        if (flying)
        {
            // This line doesn't seem to work, but we actually don't want the flying enemy to rotate anyways so it's ok
            // navigator.transform.rotation = Quaternion.LookRotation(navigator.player.transform.position);
            navigator.transform.localScale = (navigator.player.transform.position.x > navigator.transform.position.x) ? new Vector2(1, 1) : new Vector2(-1, 1);
        }
        else
        {
            navigator.transform.localScale = (navigator.player.transform.position.x > navigator.transform.position.x) ? new Vector2(1, 1) : new Vector2(-1, 1);
        }
    }
}