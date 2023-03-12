using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

// static ?
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;
    //public UnityEvent OnTransitionDone;

    // TODO: These may have to live in e.g. SceneLoaderArea if non-standardized.
    public Animator transition;
    [SerializeField] private float _transitionTimeNewScene = 3.0f;
    [SerializeField] private float _transitionTimeReload = 5f;

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
     * TODO: different reload depending on save or safe
     */
    public void loadScene(Location spawnPoint, bool force=false, Action callback=null)
    {
        Debug.Log($"SceneLoader - Loading {spawnPoint.sceneName} ({spawnPoint.x}, {spawnPoint.y})...");
        if (spawnPoint.sceneName == getCurrentSceneName() && !force)
        {
            StartCoroutine(animatedReloadScene(spawnPoint, callback));
            return;
        }
        StartCoroutine(animatedLoadScene(spawnPoint, callback));
    }

    public static string getCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    private IEnumerator animatedReloadScene(Location spawnPoint, Action callback)
    {
        transition.SetTrigger("Start");
        PauseControl.PauseGame();
        yield return new WaitForSecondsRealtime(_transitionTimeReload); // TODO: wanted this to freeze the game for 0.5s
        PlayerStatus.Instance.teleportPlayer(spawnPoint);
        callback?.Invoke();
        PauseControl.ResumeGame();
        transition.SetTrigger("End");
    }

    // to new level
    private IEnumerator animatedLoadScene(Location spawnPoint, Action callback)
    {
        transition.SetTrigger("Start");
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
            callback?.Invoke();
            PlayerStatus.Instance.teleportPlayer(spawnPoint);
            SaveAndLoader.Instance.LastSafeLocManager.setLastSafeLoc(spawnPoint); // this is duplicate when saveandloading
            PlayerStatus.Instance.UpgradeManager.applyUpgradeItemState();
            Enemy.hideEnemies(SaveAndLoader.Instance.EnemySaveManager.getKillList());
        }
        PauseControl.ResumeGame();
        transition.SetTrigger("End");
    }

    private bool isScene(string sceneName)
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
