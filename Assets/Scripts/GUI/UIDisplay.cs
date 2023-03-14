using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIDisplay : MonoBehaviour
{
    
    public static UIDisplay Instance;
    
    public Slider energySlider;
    public SpriteRenderer spriteRenderer;
    public GameObject UI;
    public CanvasGroup UIcanvas;

    private void Awake()
    {
        UI = GameObject.FindWithTag("UI");
        UIcanvas = UI.GetComponent<CanvasGroup>();
        UIcanvas.alpha = 0;
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one PlayerStatusDisplay");
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            updateEnergy(PlayerStatus.Instance.EnergyManager.getCurrentEnergy());
            Debug.Log(PlayerStatus.Instance.EnergyManager.getCurrentEnergy());
            Debug.Log(energySlider.value); 
        }
    }

    public void updateHP(int currentHP)
    {

    }

    public void updateEnergy(int currentEnergy)
    {
        //energySlider.value = rescaleEnergy(currentEnergy-20);
    }

    private int rescaleEnergy(int currentEnergy)
    {
        double temp = currentEnergy;
        return (int) Math.Round(temp*52/50);
    }

    public void showUI()
    {
        UIcanvas.alpha = 1;
    }

    public void hideUI()
    {
        UIcanvas.alpha = 0;
    }
    
    
}
