using UnityEngine;

[CreateAssetMenu(fileName= "CharacterController", menuName= "InputController/CharacterController")]
public class CharacterController : InputController
{
    public override float MoveInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public override bool JumpPress()
    {
        return Input.GetButtonDown("Jump");
    }

    public override bool JumpHold()
    {
        return Input.GetButton("Jump");
    }

    public override bool MeleePress()
    {
        return Input.GetButtonDown("Melee");
    }

    public override bool RangedPress()
    {
        return Input.GetButtonDown("Ranged");
    }

    public override bool HealPress()
    {
        return Input.GetButton("Heal");
    }
}
