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
    public CanvasGroup UIcanvas;
    public Animator[] HPAnimators;
    public bool[] hasHP;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Can only have one UIDisplay");
            Destroy(this);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        UIcanvas = GameObject.FindWithTag("UI").GetComponent<CanvasGroup>();
        UIcanvas.alpha = 0;
    }

    public void updateHP(int currentHP)
    {
        for (int i = 5; i > 0; i--)
        {
            if( currentHP - i >= 0 )
            {
                Debug.Log(i);
                HPAnimators[i-1].SetBool("hasHP", true);
                Debug.Log("has hp test");
            }
            else
            {
                HPAnimators[i-1].SetBool("hasHP", false);
            }
        }
    }
    
    public void updateEnergy(int currentEnergy)
    {
        energySlider.value = rescaleEnergy(currentEnergy);
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
    
    //TODO: remove
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PlayerStatus.Instance.HPManager.setCurrentHP(PlayerStatus.Instance.HPManager.getCurrentHP()-1);
            Debug.Log(HPAnimators[4].GetBool("hasHP"));
            Debug.Log(PlayerStatus.Instance.HPManager.getCurrentHP());
        }
    }
    
}
