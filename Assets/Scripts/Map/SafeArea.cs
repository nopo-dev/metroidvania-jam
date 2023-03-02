using UnityEngine;

public class SafeArea : CollidableArea
{
    [SerializeField] private Location _spawnPoint;

    private void Start()
    {
        // sceneName will be exposed in inspector but we don't need to change it
        _spawnPoint.sceneName = SceneLoader.Instance.getCurrentSceneName();
    }

    protected override void collisionHandler(Collider2D other)
    {
        PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(_spawnPoint);
    }
}   
