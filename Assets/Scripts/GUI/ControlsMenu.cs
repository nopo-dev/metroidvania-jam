using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsMenu : MonoBehaviour
{
    public static ControlsMenu Instance { get; private set; }

    public int sourceMenu = 0;
    public GameObject MainMenu;
    public GameObject PauseMenu;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } 
        else if (this != Instance)
        {
            Debug.Log("Destroying extra ControlsMenu");
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void backButtonClick()
    {
        if (sourceMenu == 0)
        {
            MainMenu.SetActive(true);
        }
        else if (sourceMenu == 1)
        {
            PauseMenu.SetActive(true);
        }
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public bool getActive()
    {
        return gameObject.activeSelf;
    }
}
