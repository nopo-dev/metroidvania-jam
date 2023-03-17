using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particles;
    private float _projectileSpeed = 1f;
    private Vector3 _projectileAngle = new Vector3(1f, 1f, 0f);
    private string ignoreTag;
    private Rigidbody2D _rb;
    private Vector3 velocity;

    public void SetSpeed(float speed)
    {
        _projectileSpeed = speed;
    }

    public void SetAngle(Vector3 angle)
    {
        _projectileAngle = angle;
        transform.localScale = new Vector3(transform.localScale.x * Mathf.Sign(angle.x), 1, 1);
    }

    public void SetIgnoreTag(string tag)
    {
        ignoreTag = tag;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddRelativeForce(_projectileAngle * _projectileSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(ignoreTag))
        {
            AudioManager.Instance.PlayDelayedSound("SpitImpact", 0.1f);
            Destroy(gameObject);
            Instantiate(_particles, transform.position, Quaternion.identity);
        }
    }
}
