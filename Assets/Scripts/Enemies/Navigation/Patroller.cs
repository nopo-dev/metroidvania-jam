using UnityEngine;

/*
 * Patrols horizontally between two x-positions.
 * Currently, this navManager is not very robust to getting moved
 * outside of the patrol path. It will try to return to the old path
 * instead of establishing a new one.
 */
public class Patroller : NavManager
{
    // TOOD: only x-axis and 2-point positions for now.
    // Assume start is in between them.
    [Range(0.0f, 1.0f)]
    public float speed = 1;

    public float a;
    public float b;
    public float threshold = 0;

    private float _dest;
    private float _start;
    protected GenericEnemyController _controller;

    private void Awake()
    {
        Debug.Assert(a != b, "Patroller - Must have distinct patrol points");
        _controller = GetComponent<Move>()._input as GenericEnemyController;
        _start = transform.position.x;
        _dest = a;
    }

    private bool hasReachedDest()
    {
        bool goingRight = _dest > _start;
        return goingRight ? (transform.position.x >= _dest - threshold)
            : (transform.position.x <= _dest + threshold);
    }

    public override void doPeacefulNav()
    {
        if (hasReachedDest())
        {
            _start = _dest;
            _dest = (_dest == a) ? b : a;
        }
        _controller.direction = (_dest > transform.position.x) ? speed : -speed;
    }
}