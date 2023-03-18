using UnityEngine;

public class ScalingCollider : MonoBehaviour
{
    [SerializeField] private float _scalingTime;
    [SerializeField] private float _finalSize;

    private CircleCollider2D _collider;
    private float _growthPerSec;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        _growthPerSec = (_finalSize - _collider.radius) / _scalingTime;
    }

    void Update()
    {
        _collider.radius = Utils.Clamp(_collider.radius + _growthPerSec * Time.deltaTime, 0, _finalSize);
    }
}