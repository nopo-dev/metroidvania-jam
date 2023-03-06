using UnityEngine;

public class NavManager : MonoBehaviour
{
    public float destination;
    public float threshold;

    private GenericEnemyController _controller;
    private float _start;

    void Awake()
    {
        _controller = GetComponent<Move>()._input as GenericEnemyController;
    }

    private void Start()
    {
        setDestination(GetComponent<PatrolPoints>().getNext());
    }

    public void setDestination(float dest)
    {
        _start = transform.position.x;
        destination = dest;
        setController();
    }

    public bool hasReached()
    {
        bool goingRight = destination > _start;
        return goingRight ? (transform.position.x >= destination - threshold) : (transform.position.x <= destination + threshold);
    }

    private bool withinThreshold()
    {
        return Mathf.Abs(destination - transform.position.x) <= threshold;
    }

    private void setController()
    {
        _controller.direction = (destination > transform.position.x) ? 1.0f : -1.0f;
        if (withinThreshold())
        {
            _controller.direction = 0; // do we even need this / threshold at all
        }
    }
}