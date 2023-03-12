using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool gameIsPaused = false;

    void Update()
    {
        //TODO: check if not on main menu / other checks to make sure you are allowed to pause
        if (Input.GetKeyDown(KeyCode.Escape) && !MainMenu.Instance.getActive())
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
        Time.timeScale = 0;
        gameIsPaused = true;
    }

    public static void ResumeGame()
    {
        Debug.Log("PauseControl - Resuming game.");
        Time.timeScale = 1;
        gameIsPaused = false;
    }
}