using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    // TOOD: only x-axis and 2-point positions for now.
    // Assume start is in between them.
    public float a;
    public float b;

    private float _nextPatrol;
    private float _start;

    private void Awake()
    {
        _start = transform.position.x;
        _nextPatrol = b;
    }

    public float getNext()
    {
        _nextPatrol = (_nextPatrol == a) ? b : a;
        return _nextPatrol;
    }
}