using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused { get; private set; } = true;
    public static bool playerIsPaused { get; private set; } = false;

    void Update()
    {
        //TODO: other checks to make sure you are allowed to pause?
        if (Input.GetKeyDown(KeyCode.Escape) && SceneLoader.getCurrentSceneName() != "Menu Scene Test")
        {
            if (PauseControl.gameIsPaused)
            {
                PauseMenu.Instance.Resume();
            }
            else
            {
                PauseMenu.Instance.Pause();
            }
        }
    }

    public static void PausePlayer()
    {
        playerIsPaused = true;
    }

    public static void ResumePlayer()
    {
        playerIsPaused = false;
    }

    public static void PauseGame()
    {
        Debug.Log("PauseControl - Pausing game.");
        UIDisplay.Instance.hideUI();
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public static void ResumeGame()
    {
        Debug.Log("PauseControl - Resuming game.");
        UIDisplay.Instance.showUI();
        UIDisplay.Instance.controlMenu.SetActive(false);
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}