using UnityEngine;

/*
 * Written exclusively for SnailMan, not widely adaptable as of yet.
 */
[CreateAssetMenu(menuName = "NavManager/Decider")]
public class Decider : NavManager
{
    [SerializeField] public float thinkingTime;
    [HideInInspector] public float startTime;
    [HideInInspector] public float uncountedTime;

    public override void doPeacefulNav(Enemy navigator) {
        SnailMan snailMan = navigator as SnailMan;
        if (snailMan.deciding) { return; }
        Debug.Log("Decider - Starting decision timer.");
        uncountedTime = 0;
        startTime = Time.time;
        snailMan.deciding = true;
    }

    public bool hasDecided()
    {
        return Time.time - startTime - uncountedTime > thinkingTime;
    }
}