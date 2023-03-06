using UnityEngine;

public class SightSensor : MonoBehaviour
{
    [SerializeField] private float _sightThreshold = 10; // Why do these feel like different units than shown in inspector
    [SerializeField] private float _lossThreshold = 15; // Loss > Sight for more rubber-bandy chasing

    public GameObject Player { get; private set; }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
    }

    public float getPlayerLoc()
    {
        return Player.transform.position.x;
    }

    public bool ping()
    {
        return Vector2.Distance(transform.position, Player.transform.position) < _sightThreshold;
    }

    public bool unping()
    {
        return Vector2.Distance(transform.position, Player.transform.position) > _lossThreshold;
    }
}