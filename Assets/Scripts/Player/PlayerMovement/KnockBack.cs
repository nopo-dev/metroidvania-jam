using System.Collections;
using UnityEngine;

/*
 * idk this could probably be static
 */
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float _knockbackSpeed;
    [SerializeField] private float _iseconds;
    private Rigidbody2D _playerBody;
    private Collider2D _playerTriggerCollider;

    // Use this for initialization
    void Awake()
    {
        _playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _playerTriggerCollider = GameObject.FindWithTag("PlayerTrigger").GetComponent<Collider2D>();

    }

    public void knockPlayer(Collider2D knocker)
    {
        if (knocker != null)
        {
            _playerBody.velocity = _knockbackSpeed *
                (_playerBody.gameObject.transform.position - knocker.gameObject.transform.position).normalized;
            StartCoroutine(iframes());
        }
    }

    private IEnumerator iframes()
    {
        _playerTriggerCollider.isTrigger = false;
        PauseControl.PausePlayer();
        yield return new WaitForSeconds(_iseconds);
        _playerTriggerCollider.isTrigger = true;
        PauseControl.ResumePlayer();
    }
}