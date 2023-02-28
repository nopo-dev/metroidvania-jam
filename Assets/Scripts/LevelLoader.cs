using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : CollidableArea
{
    public Animator transition;
    [SerializeField] private float transitionTime = 1f;
    [SerializeField] private string _targetSceneName;
    [SerializeField] private Location _spawnPoint;

    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadNextLevel();
        }
    }*/

    protected override void collisionHandler(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Debug.Log("collided with player");
            LoadNextLevel();
            SaveAndLoader.Instance.teleportPlayer(_spawnPoint);
        }
    }

    // I followed a tutorial for this "StartCoroutine" stuff, apparently necessary for animations?
    public void LoadNextLevel()
    {
        Debug.Log("loadnextlevel");
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        Debug.Log("test");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(_targetSceneName);
    }

}
