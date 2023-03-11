using UnityEngine;

public class Bouncer : NavManager
{
    [Range(0.0f, 1.0f)]
    public float bounceSpeed = 1;
    public Vector2 startingDirection = new Vector2(1, 1);
    
    private Fly _flyer;

    void Awake()
    {
        startingDirection.Normalize();
    }

    private void Start()
    {
        _flyer = GetComponent<Fly>();
        _flyer.setDirection(bounceSpeed * startingDirection);
    }

    public override void doPeacefulNav()
    {
        _flyer.setDirection(bounceSpeed * _flyer.direction.normalized);
    }
}