using UnityEngine;

[CreateAssetMenu(fileName="CharacterController", menuName="InputController/CharacterController")]
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
}
