using UnityEngine;

public class SafeArea : CollidableArea
{
    [SerializeField] private Location _spawnPoint;

    protected override void collisionHandler(Collider2D other)
    {
        PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(_spawnPoint);
    }
}   
