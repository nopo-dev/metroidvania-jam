using UnityEngine;

[CreateAssetMenu(fileName="GenericEnemyController", menuName= "InputController/GenericEnemyController")]
public class GenericEnemyController : InputController
{
    public float direction;
    // TODO: random movement for now. needs to detect player & react
    public override float MoveInput()
    {
        return direction;
    }

    public override bool JumpPress()
    {
        return false;
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
}
