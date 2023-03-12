using UnityEngine;

[CreateAssetMenu(menuName = "NavManager/Idler")]
public class Idler : NavManager
{
    // TODO: trigger idle animation
    public override void doPeacefulNav(Enemy navigator) 
    {
        base.standStill(navigator);
    }
}