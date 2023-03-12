using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Location _nextSceneSpawnPoint;

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
