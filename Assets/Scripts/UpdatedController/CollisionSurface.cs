using UnityEngine;

public class CollisionSurface : MonoBehaviour
{
    public bool Grounded { get; private set; }
    public bool Walled { get; private set;}
    public float Friction { get; private set; }
    public Vector2 Normal { get; private set; }

    private PhysicsMaterial2D _material;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        GetFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        GetFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Friction = 0;
        Grounded = false;
        Walled = false;
    }

    public void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Normal = collision.GetContact(i).normal;
            Grounded = Normal.y >= 0.9f;
            if (Grounded)
            {
                Walled = false;
                return;
            }
            Walled = Mathf.Abs(Normal.x) >= 0.9f && !Grounded;
        }
    }

    private void GetFriction(Collision2D collision)
    {
        _material = collision.rigidbody.sharedMaterial;
        Friction = 0;

        if (_material != null)
        {
            Friction = _material.friction;
        }
    }
}
