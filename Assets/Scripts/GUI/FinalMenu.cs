using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMenu : MonoBehaviour
{
    public static FinalMenu Instance { get; private set; }

    public CanvasGroup finalMenuCanvas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one FinalMenu!");
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        finalMenuCanvas.alpha = 0;
    }

    public void show()
    {
        finalMenuCanvas.alpha = 1;
        PauseControl.PauseGame();
    }
}
