using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UpgradeChoice : MonoBehaviour
{

    [SerializeField]
    public Upgrade upgrade;

    public TextMeshProUGUI upgradeName;
    public TextMeshProUGUI upgradeDescription;
    public TextMeshProUGUI upgradeActions;
    
    public UnityEngine.UI.Image upgradeIcon;

    public UpgradeManager upgradeManager;

    public void SetUpgrade(Upgrade upgrade)
    {
        this.upgrade = upgrade;
        upgradeName.text = upgrade.Title;
        upgradeDescription.text = upgrade.Description;
        var upgradeLabels = "";
        foreach (var label in upgrade.ModifierLabels)
        {
            if (label == null || label == "")
                continue;
            upgradeLabels += label[0] == '+' 
            ? $"<color=#00DD00>{label.Substring(1)}</color>\n" // Green for '+'
            : label[0] == '-' 
            ? $"<color=#DD0000>{label.Substring(1)}</color>\n" // Red for '-'
            : $"{label}\n"; // Default color for others
            
        }
        upgradeActions.text = upgradeLabels;

    }

    public void AddUpgrade(){
        upgradeManager.addUpgrade(upgrade);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUpgrade(upgrade);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
