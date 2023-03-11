using System.Collections;
using UnityEngine;

public class Fly : MonoBehaviour
{
    [HideInInspector]
    public Vector2 direction { get; private set; }
    public float speed = 5;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0;
    }

    public void setDirection(Vector2 direction)
    {
        this.direction = direction.normalized;
        _rb.velocity = direction * speed;
    }
}