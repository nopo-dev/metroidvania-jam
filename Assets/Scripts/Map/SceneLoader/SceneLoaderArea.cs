using System.Collections;
using UnityEngine;

public class SceneLoaderArea : CollidableArea
{
    [SerializeField] private string _targetSceneName;
    [SerializeField] private Location _spawnPoint;

    protected override void collisionHandler(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SceneLoader.Instance.loadScene(_targetSceneName);
            SaveAndLoader.Instance.teleportPlayer(_spawnPoint);
            PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(_spawnPoint);
        }
    }
}
