using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused = true;

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