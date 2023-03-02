using UnityEngine;

public abstract class InputController : ScriptableObject
{
    public abstract float MoveInput();
    public abstract bool JumpPress();
    public abstract bool JumpHold();
}
