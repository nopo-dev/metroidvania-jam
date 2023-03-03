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
        PauseControl.PauseGame();
        yield return new WaitForSecondsRealtime(_transitionTimeReload); // TODO: wanted this to freeze the game for 0.5s 
        PlayerStatus.Instance.teleportPlayer(spawnPoint);
        PauseControl.ResumeGame();
    }

    private IEnumerator animatedLoadScene(Location spawnPoint)
    {
        // transition.SetTrigger("Start"); // TODO: figure this out
        PauseControl.PauseGame();

        yield return new WaitForSecondsRealtime(_transitionTimeNewScene);

        if (!isScene(spawnPoint.sceneName))
        {
            Debug.Log($"SceneLoader - {spawnPoint.sceneName} is not a valid scene. Will not load a scene."); // TODO: log that dynamically grabs class name
        } 
        else
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(spawnPoint.sceneName);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            PlayerStatus.Instance.teleportPlayer(spawnPoint);
            PlayerStatus.Instance.LastSafeLocManager.setLastSafeLoc(spawnPoint); // this is duplicate when saveandloading
        }
        PauseControl.ResumeGame();
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
