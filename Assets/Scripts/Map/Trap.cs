using UnityEngine;

public class Trap : CollidableArea
{
    [SerializeField] private int damage_;

    protected override void collisionHandler(Collider2D other)
    {
        PlayerStatus.Instance.HPManager.damageHP(this.damage_);
        SceneLoader.Instance.loadScene(PlayerStatus.Instance.LastSafeLocManager.getLastSafeLoc());
        // TODO: might also want to trigger some animation here
    }

}
