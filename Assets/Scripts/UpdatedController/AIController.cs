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
}
