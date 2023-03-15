using UnityEngine;

[CreateAssetMenu(fileName="AIController", menuName="InputController/AIController")]
public class AIController : InputController
{
    // template AI input controller
    public override float MoveInput()
    {
        return 1f;
    }

    public override bool JumpPress()
    {
        return true;
    }

    public override bool JumpHold()
    {
        return false;
    }

    public override bool MeleePress()
    {
        throw new System.NotImplementedException();
    }

    public override bool RangedPress()
    {
        throw new System.NotImplementedException();
    }

    public override bool HealPress()
    {
        throw new System.NotImplementedException();
    }
}
