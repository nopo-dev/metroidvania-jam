using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Location _nextSceneSpawnPoint;

    public static MainMenu Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one MainMenu!");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //TODO: change so that location is pulled from save data 
    public void PlayButton()
    {
        SceneLoader.Instance.loadScene(_nextSceneSpawnPoint);
    }

    public void QuitButton()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
