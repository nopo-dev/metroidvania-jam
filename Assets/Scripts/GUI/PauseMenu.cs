using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one PauseMenu!");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Pause()
    {
        PauseControl.PauseGame();
        gameObject.SetActive(true);
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        PauseControl.ResumeGame();
    }

    public void QuitButton()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    // TODO: Change default location to finalized version scene
    public void QuitToTitleButton()
    {
        Location defaultLocation = new Location(0, 0, "Menu Scene Test");
        Debug.Log(defaultLocation.sceneName);
        Debug.Log(UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings);
        SceneManager.LoadScene("Menu Scene Test");
        PauseControl.PauseGame();
    }
}
