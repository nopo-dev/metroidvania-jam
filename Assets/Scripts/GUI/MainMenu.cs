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
        AudioManager.Instance.PlaySound("MainMenuTheme");
    }

    //TODO: change so that location is pulled from save data 
    public void PlayButton()
    {
        SaveAndLoader.Instance.load();
        AudioManager.Instance.FadeOut("MainMenuTheme", 1f);
        AudioManager.Instance.PlayDelayedSound("BackgroundTheme", 2f);
    }

    public void QuitButton()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public bool getActive()
    {
        return gameObject.activeSelf;
    }
    //TODO: add another button that says resume rather than play if savedata exists

    public void ControlsButton()
    {
        ControlsMenu.Instance.sourceMenu = 0;
    }

}
