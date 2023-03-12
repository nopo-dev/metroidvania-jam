using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _movementScale = 0.5f;
    [SerializeField] private float _movementOffset = -0.5f;

    private List<Transform> _backgroundLayers = new List<Transform>();
    private Vector3 _tilemapCenter;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _backgroundLayers.Add(transform.GetChild(i));
        }
    }

    private void Update()
    {
        for (int i = 0; i < _backgroundLayers.Count; i++)
        {
            Vector3 position = _backgroundLayers[i].position;
            position.x = Mathf.Lerp(_camera.position.x, transform.position.x,
                (float)i / _backgroundLayers.Count * _movementScale + _movementOffset);
            position.y = Mathf.Lerp(_camera.position.y, transform.position.y,
                (float)i / _backgroundLayers.Count * _movementScale + _movementOffset);

            _backgroundLayers[i].position = position;
        }
    }

}
