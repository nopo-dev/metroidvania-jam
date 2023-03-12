using System;
using System.Collections;

/*
 * This is a (maybe temporary) enemy object,
 * for testing HP management and save/load.
 * Be careful about inheriting it.
 */
public class Minion : Enemy
{
    // TODO: FSM for is hit, attacking, dying, go back to idle, etc. things
    // TODO: enemy shouldn't respect collision with player

    protected override IEnumerator dyingAnimation(Action callback)
    {
        // Dying anim to go here
        yield return null;
    }

}
