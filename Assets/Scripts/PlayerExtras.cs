using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PlayerInput
{
    public float X;
    public bool JumpPressed;
    public bool JumpReleased;
}

public struct RayRange {
    public RayRange(float x1, float y1, float x2, float y2, Vector2 direction) {
        Start = new Vector2(x1, y1);
        End = new Vector2(x2, y2);
        Direction = direction;
    }

    public readonly Vector2 Start, End, Direction;
}