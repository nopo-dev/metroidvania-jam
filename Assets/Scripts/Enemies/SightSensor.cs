using UnityEngine;

public class SightSensor : MonoBehaviour
{
    [SerializeField] private float _sightThreshold = 10; // Why do these feel like different units than shown in inspector

    public GameObject Player { get; private set; }
    private float startTime;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        startTime = Time.time;
    }

    public float getPlayerLoc()
    {
        return Player.transform.position.x;
    }

    public bool ping()
    {
        return Vector2.Distance(transform.position, Player.transform.position) < _sightThreshold;
    }
}