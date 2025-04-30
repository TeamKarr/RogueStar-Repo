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
        upgrade = manager.defaultAttributes["MaxSpeed"];
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
        buttonLabel.text = "UPGRADE\n(" + upgrade.UpgradeAmount.ToString("F1") + " Gold)";
    }

    public void Upgrade()
    {
        upgrade.amount += upgrade.UpgradeAmount;
        upgrade.amount = Mathf.Clamp(upgrade.amount, slider.minValue, slider.maxValue);
        upgrades++;
        if (upgrades >= upgrade.MaxUpgrades)
        {
            button.interactable = false;
        }
        updateDisplay();
    }


}
