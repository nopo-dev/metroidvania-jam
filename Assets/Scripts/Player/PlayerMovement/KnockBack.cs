using System.Collections;
using UnityEngine;

/*
 * idk this could probably be static
 */
public class KnockBack : MonoBehaviour
{
    [SerializeField] private float _knockbackSpeed;
    [SerializeField] private float _iseconds;
    [SerializeField] private float _degreesUp;
    [SerializeField] private float _ibuffer;
    private Rigidbody2D _playerBody;
    private Collider2D _playerTriggerCollider;
    private Vector2 _knockLeft;
    private Vector2 _knockRight;
    // Use this for initialization
    void Awake()
    {
        _playerBody = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>();
        _playerTriggerCollider = GameObject.FindWithTag("PlayerTrigger").GetComponent<Collider2D>();
        var rot = Quaternion.AngleAxis(_degreesUp, Vector3.forward);
        _knockLeft = Quaternion.Inverse(rot) * Vector2.left;
        _knockRight = rot * Vector2.right;
    }

    public void knockPlayer(Vector2 knocker)
    {
        if (knocker != null)
        {
            StartCoroutine(iframes(knocker));
        }
    }

    private IEnumerator iframes(Vector2 knocker)
    {
        _playerTriggerCollider.enabled = false;
        //PauseControl.PausePlayer();

        // this doesn't strictly have to live in the coroutine
        // but i might've seen a race condition with this vs PausePlayer
        bool left = _playerBody.gameObject.transform.position.x < knocker.x;
        bool flipped = _playerBody.gameObject.transform.localScale.x == 1;

        left = left ^ flipped;

        _playerBody.velocity = _knockbackSpeed * (left ? _knockLeft : _knockRight);

        yield return new WaitForSeconds(_iseconds);
        //PauseControl.ResumePlayer();

        yield return new WaitForSeconds(_ibuffer);
        _playerTriggerCollider.enabled = true;
    }
}