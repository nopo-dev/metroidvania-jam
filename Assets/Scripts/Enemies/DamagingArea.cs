
using UnityEngine;

public class DamagingArea : CollidableArea
{
    [SerializeField] private int _damage;
    protected override void collisionHandler(Collider2D other)
    {
        if (other.tag == "PlayerTrigger")
        {
            PlayerStatus.Instance.HPManager.damageHP(_damage, other.gameObject.transform.position);
        }
    }
}