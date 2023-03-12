using System;
using UnityEngine;

/*
 * Patrols horizontally between two x-positions.
 * Currently, this navManager is not very robust to getting moved
 * outside of the patrol path. It will try to return to the old path
 * instead of establishing a new one.
 */
[CreateAssetMenu(menuName = "NavManager/Patroller")]
public class Patroller : NavManager
{
    // TOOD: only x-axis and 2-point positions for now.
    // Assume start is in between them.
    [Range(0.0f, 1.0f)]
    public float speed = 1;

    public float threshold = 0;

    private bool hasReachedDest(Vector2 start, Vector2 loc, Vector2 dest)
    {
        if (!flying)
        {
            bool goingRight = dest.x > start.x;
            return goingRight ? (loc.x >= dest.x - threshold)
                : (loc.x <= dest.x + threshold);
        }
        else
        {
            throw new NotImplementedException("Didn't implement flying patroller yet");
        }
    }

    public override void doPeacefulNav(Enemy navigator)
    {
        if (hasReachedDest(navigator.startLoc, navigator.transform.position, navigator.destinationLoc))
        {
            navigator.startLoc = navigator.destinationLoc;
            navigator.destinationLoc = (navigator.destinationLoc == navigator.patrolA) ? navigator.patrolB : navigator.patrolA;
        }
        base.moveTowards(navigator, navigator.destinationLoc, speed);
    }
}