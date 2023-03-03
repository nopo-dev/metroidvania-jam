using UnityEngine;

public static class PauseControl
{
    public static bool gameIsPaused = false;

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