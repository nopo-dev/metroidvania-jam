using System.Collections;
using UnityEngine;

public class SceneLoaderArea : CollidableArea
{
    [SerializeField] private Location _nextSceneSpawnPoint;

    protected override void collisionHandler(Collider2D other)
    {
        if (other.tag == "PlayerTrigger")
        {
            SceneLoader.Instance.loadScene(_nextSceneSpawnPoint);
        }
    }
}
