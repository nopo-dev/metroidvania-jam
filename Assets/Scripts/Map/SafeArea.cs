using UnityEngine;

public class SafeArea : CollidableArea
{
    // TODO: we may need to expose this to inspector, if we want non-centered
    // spawn points.
    private Location _spawnPoint;

    private void Start()
    {
        _spawnPoint.sceneName = SceneLoader.Instance.getCurrentSceneName();
        _spawnPoint.x = this.transform.position.x;
        _spawnPoint.y = this.transform.position.y;
    }

    protected override void collisionHandler(Collider2D other)
    {
        PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(_spawnPoint);
    }
}   
