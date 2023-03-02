using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
     * Used when loading a new scene
     */
    public void loadScene(string sceneName)
    {
        Debug.Log($"SceneLoader - Loading {sceneName}...");
        if (sceneName == getCurrentSceneName())
        {
            reloadScene();
            return;
        }
        StartCoroutine(animatedLoadScene(sceneName));
    }

    /*
     * Used for a transition within the same scene, e.g. traps.
     * For now, just delays for a bit.
     */
    public void reloadScene()
    {
        StartCoroutine(animatedRefresh());
    }

    public string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    private IEnumerator animatedRefresh()
    {
        yield return new WaitForSeconds(_transitionTimeReload); // TODO: wanted this to freeze the game for 0.5s but it doesn't work
    }

    private IEnumerator animatedLoadScene(string sceneName)
    {
        // transition.SetTrigger("Start"); // TODO: figure this out

        yield return new WaitForSeconds(_transitionTimeNewScene);

        if (!isScene(sceneName))
        {
            Debug.Log($"SceneLoader - {sceneName} is not a valid scene. Falling back to 0-index scene."); // TODO: log that dynamically grabs class name
            SceneManager.LoadScene(getSceneNames()[0]);
        } 
        else
        {
            SceneManager.LoadScene(sceneName);
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
            Debug.Log(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
        }
        return scenesInBuild;
    }
}
