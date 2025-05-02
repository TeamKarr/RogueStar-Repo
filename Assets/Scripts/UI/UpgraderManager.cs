using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class UpgraderManager : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI title;
    private string titleText;
    public TextMeshProUGUI buttonLabel;
    public Button button;

    public GameManager manager;

    private AttributeUpgrade upgrade;

    private int upgrades = 0;

    public string upgradeName;

    public void Start()
    {
        //float amount = manager.GetAttribute(upgradeName);\
        Debug.Log(manager);
        upgrade = manager.defaultAttributes[upgradeName];
        titleText = title.text;
        slider.minValue = upgrade.amount;
        slider.maxValue = upgrade.amount + upgrade.UpgradeAmount*upgrade.MaxUpgrades;

        updateDisplay();

    }

    public void updateDisplay()
    {
        Debug.Log("updated amount");
        slider.value = upgrade.amount;
        title.text = titleText + " " + upgrade.amount.ToString("F1");
        buttonLabel.text = "UPGRADE\n(" + upgrade.Cost.ToString("F1") + " M)";
        if (upgrades >= upgrade.MaxUpgrades)
        {
            buttonLabel.text = "MAX\nUPGRADES";
            button.interactable = false;
        }
        else if (manager.Matter < upgrade.Cost)
        {
            buttonLabel.color = Color.red;
            button.interactable = false;
        }
        else
        {
            buttonLabel.color = Color.black;
            button.interactable = true;
        }
    }

    public void Upgrade()
    {
        if (manager.Matter < upgrade.Cost)
        {
            Debug.Log("Not enough matter");
            updateDisplay();
            return;
        }
        manager.Matter -= (int)upgrade.Cost;
        upgrade.Cost += upgrade.CostIncrease;
        upgrade.amount += upgrade.UpgradeAmount;
        upgrade.amount = Mathf.Clamp(upgrade.amount, slider.minValue, slider.maxValue);
        upgrades++;
        
        updateDisplay();
    }


}
