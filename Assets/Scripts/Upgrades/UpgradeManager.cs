using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class UpgradeManager : MonoBehaviour
{

    public List<Upgrade> upgrades = new();

    private AttributeManager attributeManager;

    public UpgradeChoice upgradeChoice1;
    public UpgradeChoice upgradeChoice2;
    public UpgradeChoice upgradeChoice3;


    public void addUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        foreach (var modifier in upgrade.AttributeModifiers)
        {
            attributeManager.getAttribute(modifier.attribute).addModifier(modifier.modifier);
        }
    }

    public void removeUpgrade(Upgrade upgrade)
    {
        upgrades.Remove(upgrade);
        foreach (var modifier in upgrade.AttributeModifiers)
        {
            attributeManager.getAttribute(modifier.attribute).removeModifier(modifier.modifier);
        }
    }

    public List<Upgrade> allUpgrades = new();
    [ReadOnly] public List<Upgrade> availableUpgrades;
    private List<float> culmitiveWeights = new();


    public void SetUpgradeChoices()
    {
        
        var upgrade1 = getRandomUpgrade();
        var upgrade2 = getRandomUpgrade();
        var upgrade3 = getRandomUpgrade();
        upgradeChoice1.SetUpgrade(upgrade1);
        upgradeChoice2.SetUpgrade(upgrade2);
        upgradeChoice3.SetUpgrade(upgrade3);
        
    }

    Upgrade getRandomUpgrade(){
        // pick a random upgrade from the available upgrades using the float in the tuple as the weight:
        var value = Random.Range(0,culmitiveWeights.Last());
        int index = culmitiveWeights.BinarySearch(value);
        if (index < 0)
            index = ~index;
        Debug.Log(index);
        return availableUpgrades[index];
    }

    // Use this for initialization
    void Start()
    {
        culmitiveWeights.Add(0);
        attributeManager = GetComponent<AttributeManager>();
        foreach (var upgrade in allUpgrades)
        {
            availableUpgrades.Add(upgrade);
            culmitiveWeights.Add(culmitiveWeights.Last()+upgrade.weight);
        }
        culmitiveWeights.RemoveAt(0);
        Debug.Log(culmitiveWeights);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
