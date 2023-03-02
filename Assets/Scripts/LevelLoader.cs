using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : CollidableArea
{
    public Animator transition;
    [SerializeField] private float _transitionTime = 1f;
    [SerializeField] private string _targetSceneName;
    [SerializeField] private Location _spawnPoint;

    protected override void collisionHandler(Collider2D other)
    {
        if (other.tag == "Player")
        {
            LoadNextLevel();
            // TODO: change so that player status knows where to spawn player in new scene.
            SaveAndLoader.Instance.teleportPlayer(_spawnPoint);
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        SceneManager.LoadScene(_targetSceneName);
    }

}
