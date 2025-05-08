using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerExperience : MonoBehaviour
{
    [HideInInspector] public float expValue = 0;
    public Slider ExpBar;

    public UIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ExpGain(float exp)
    {
        expValue += exp;
        if (expValue >= ExpBar.maxValue)
        {
            manager.ShowUpgradePanel();
            expValue = 0;
        }
        updateExpBar();
    }
    public void updateExpBar()
    {
        if (ExpBar != null)
        {
            ExpBar.value = expValue;
        }
    }
}