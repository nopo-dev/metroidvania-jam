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
            SaveAndLoader.Instance.teleportPlayer(_spawnPoint);
        }
    }

    // I followed a tutorial for this "StartCoroutine" stuff, apparently necessary for animations?
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
