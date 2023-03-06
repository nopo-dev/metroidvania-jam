using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxRenderer : MonoBehaviour
{
    private Vector3 bounds, offset;
    private float radius;
    
    private void Start()
    {
        if (GetComponent<BoxCollider2D>() != null)
        {
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            bounds = box.size;
            offset = box.offset;
        }
        else if (GetComponent<CircleCollider2D>() != null)
        {
            CircleCollider2D circle = GetComponent<CircleCollider2D>();
            radius = circle.radius;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (GetComponent<BoxCollider2D>() != null)
            Gizmos.DrawWireCube(transform.position + offset, bounds);
        else if (GetComponent<CircleCollider2D>() != null)
            Gizmos.DrawWireSphere(transform.position, radius);
    }
}
