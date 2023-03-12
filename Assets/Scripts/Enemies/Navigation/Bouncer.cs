using UnityEngine;

[CreateAssetMenu(menuName = "NavManager/Bouncer")]
public class Bouncer : NavManager
{
    [Range(0.0f, 1.0f)]
    public float floatSpeed = 1;
    

    void Awake()
    {
        base.Awake();
        Debug.Assert(flying, "Only flyers can Bounce");
    }

    public override void doPeacefulNav(Enemy navigator)
    {
        navigator.flyer.setDirection(floatSpeed * navigator.flyer.direction.normalized);
    }

    public override void startNav(Enemy navigator, Vector2 direction)
    {
        navigator.flyer.setDirection(floatSpeed * direction.normalized);
    }
}