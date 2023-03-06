using UnityEngine;

public class NavManager : MonoBehaviour
{
    public float destination;

    private GenericEnemyController _controller;

    void Awake()
    {
        _controller = GetComponent<Move>()._input as GenericEnemyController;
    }

    private void Start()
    {
        destination = GetComponent<PatrolPoints>().getNext();
        _controller.direction = (destination > transform.position.x) ? 1.0f : -1.0f;
    }

    public void setDestination(float dest)
    {
        destination = dest;
        _controller.direction = (destination > transform.position.x) ? 1.0f : -1.0f;
    }
}