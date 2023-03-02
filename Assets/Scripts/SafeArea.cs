using UnityEngine;

public class SafeArea : CollidableArea
{
    private Location _spawnPoint;

    void Start()
    {
        _spawnPoint.x = this.transform.position.x;
        _spawnPoint.y = this.transform.position.y;
    }

    protected override void collisionHandler(Collider2D other)
    {
        PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(_spawnPoint);
    }
}   
