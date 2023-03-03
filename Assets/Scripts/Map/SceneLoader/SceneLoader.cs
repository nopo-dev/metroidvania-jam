using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// static ?
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    // TODO: These may have to live in e.g. SceneLoaderArea if non-standardized.
    public Animator transition;
    [SerializeField] private float _transitionTimeNewScene = 1.0f;
    [SerializeField] private float _transitionTimeReload = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one SceneLoader!");
            Destroy(this);
            return;
        }
        Instance = this;
    }

    /*
     * Used when loading a new scene including spawnPoint
     */
    public void loadScene(Location spawnPoint)
    {
        Debug.Log($"SceneLoader - Loading {spawnPoint.sceneName} ({spawnPoint.x}, {spawnPoint.y})...");
        if (spawnPoint.sceneName == getCurrentSceneName())
        {
            StartCoroutine(animatedReloadScene(spawnPoint));
            return;
        }
        StartCoroutine(animatedLoadScene(spawnPoint));
    }

    public static string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    private IEnumerator animatedReloadScene(Location spawnPoint)
    {
        // transition.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionTimeReload); // TODO: wanted this to freeze the game for 0.5s 
        SaveAndLoader.Instance.teleportPlayer(spawnPoint);
    }

    private IEnumerator animatedLoadScene(Location spawnPoint)
    {
        // transition.SetTrigger("Start"); // TODO: figure this out

        yield return new WaitForSeconds(_transitionTimeNewScene);

        if (!isScene(spawnPoint.sceneName))
        {
            Debug.Log($"SceneLoader - {spawnPoint.sceneName} is not a valid scene. Falling back to 0-index scene."); // TODO: log that dynamically grabs class name
            SceneManager.LoadScene(getSceneNames()[0]);
        } 
        else
        {
            SceneManager.LoadScene(spawnPoint.sceneName);
            // TODO: player continues to exist before being teleported; potential for issues here, e.g. momentarily being inside a wall.
            // TOOD: since LoadScene takes a second, player is briefly shown teleported to the spawn point coords in the old scene. Maybe not a problem in release build.
            SaveAndLoader.Instance.teleportPlayer(spawnPoint);
            PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(spawnPoint); // this is duplicate when saveandloading
        }
    }

    private Boolean isScene(string sceneName)
    {
        return getSceneNames().Contains(sceneName);
    }

    private List<string> getSceneNames()
    {
        List<string> scenesInBuild = new List<string>();
        for (int i = 0; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; i++)
        {
            scenesInBuild.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
        }
        return scenesInBuild;
    }
}
