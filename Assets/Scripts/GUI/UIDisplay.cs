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

    private void Awake()
    {
        //TODO: set Slider value to last energy value from save file
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one PlayerStatusDisplay");
            Destroy(this);
        }
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            updateEnergy();
            Debug.Log(PlayerStatus.Instance.EnergyManager.getCurrentEnergy());
        }
    }

    public void updateHP()
    {

    }

    public void updateEnergy()
    {
        PlayerStatus.Instance.EnergyManager.setCurrentEnergy(PlayerStatus.Instance.EnergyManager.getCurrentEnergy()-4);
        energySlider.value = rescaleEnergy(PlayerStatus.Instance.EnergyManager.getCurrentEnergy()-20);
    }

    private int rescaleEnergy(int currentEnergy)
    {
        double temp = currentEnergy;
        return (int) Math.Round(temp*52/50);
    }

}
