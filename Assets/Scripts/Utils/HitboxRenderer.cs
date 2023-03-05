using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRenderer : MonoBehaviour
{
    private Vector3 bounds, offset;
    
    private void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        bounds = collider.size;
        offset = collider.offset;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + offset, bounds);
    }
}
