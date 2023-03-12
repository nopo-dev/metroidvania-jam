using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
