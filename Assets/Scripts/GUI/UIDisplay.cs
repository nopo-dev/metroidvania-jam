using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIDisplay : MonoBehaviour
{
    
    public static UIDisplay Instance;
    
    public Slider energySlider;
    public CanvasGroup UIcanvas;
    public Animator[] HPAnimators;

    private float desiredDuration = 0.25f;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        } 
        else if (this != Instance)
        {
            Debug.Log("Destroying extra UIDisplay");
            Destroy(this);
        }
        DontDestroyOnLoad(gameObject);
        //UIcanvas = GameObject.FindWithTag("UI").GetComponent<CanvasGroup>();
        UIcanvas.alpha = 0;
        //HPAnimators = GameObject.FindWithTag("HPbar").GetComponentsInChildren<Animator>();
    }

    public void updateHP(int currentHP)
    {
        // probably a better way to do this
        for (int i = 5; i > 0; i--)
        {
            if( currentHP - i >= 0 )
            {
                HPAnimators[i-1].SetBool("hasHP", true);
            }
            else
            {
                HPAnimators[i-1].SetBool("hasHP", false);
            }
        }
    }
    
    public void updateEnergy(int currentEnergy, int newCurrentEnergy)
    {
        Debug.Log(currentEnergy);

        StartCoroutine(lerp((float) currentEnergy, (float) newCurrentEnergy, desiredDuration));
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

    public IEnumerator lerp(float startEnergy, float endEnergy, float overTime)
    {
        float startTime = Time.time;
        while(Time.time < startTime + overTime)
        {
            energySlider.value = Mathf.Lerp(startEnergy, endEnergy, (Time.time - startTime)/overTime);
            yield return null;
        }
        energySlider.value = endEnergy;
    }
    
}
