using UnityEngine;

[CreateAssetMenu(menuName = "NavManager/Decider")]
public class Decider : NavManager
{
    private float _startTime;

    public override void doPeacefulNav(Enemy navigator) {
        SnailMan snailMan = navigator as SnailMan;
        if (snailMan.deciding) { return; }

        _startTime = Time.time;
    }
}