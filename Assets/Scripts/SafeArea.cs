using UnityEngine;

public class SafeArea : CollidableArea
{
    [SerializeField] private float spawnLocationX_; 
    [SerializeField] private float spawnLocationY_;

    protected override void collisionHandler(Collider2D other)
    {
        PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(new Location(spawnLocationX_,spawnLocationY_));
    }
}   
