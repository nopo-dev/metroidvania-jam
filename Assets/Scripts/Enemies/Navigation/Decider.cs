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
        SnailMan snailman = navigator as SnailMan;
        snailman.facePlayer();
        if (snailman.deciding) { return; }
        Debug.Log("SnailMan - Starting decision timer.");
        uncountedTime = 0;
        startTime = Time.time;
        snailman.deciding = true;
    }

    public bool hasDecided()
    {
        //Debug.Log($"{startTime}, {uncountedTime}");
        return Time.time - startTime - uncountedTime > thinkingTime;
    }
}