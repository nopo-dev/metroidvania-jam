using UnityEngine;

public class PatrolPoints : MonoBehaviour
{
    // TOOD: only x-axis and 2-point positions for now.
    // Assume start is in between them.
    public float a;
    public float b;

    private float _nextPatrol;
    private float _currentLoc;
    private float _start;

    private void Awake()
    {
        _start = transform.position.x;
        _nextPatrol = b;
    }

    private void Update()
    {
        _currentLoc = transform.position.x;
    }

    public bool hasReached(NavManager nav)
    {
        return Mathf.Abs(_currentLoc - nav.destination) >= Mathf.Abs(_start - nav.destination);
    }

    public float getNext()
    {
        _nextPatrol = (_nextPatrol == a) ? b : a;
        return _nextPatrol;
    }
}