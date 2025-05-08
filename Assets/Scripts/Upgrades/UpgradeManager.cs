using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class UpgradeManager : MonoBehaviour
{
    
    [ReadOnly] public List<Upgrade> upgrades = new();

    private AttributeManager attributeManager;

    public UpgradeChoice upgradeChoice1;
    public UpgradeChoice upgradeChoice2;
    public UpgradeChoice upgradeChoice3;


    public void addUpgrade(Upgrade upgrade)
    {
        upgrades.Add(upgrade);
        upgrade.Initialise(gameObject);
    }

    public void removeUpgrade(Upgrade upgrade)
    {
        upgrades.Remove(upgrade);
        
    }

    public string upgradePath = "Assets/Upgrades";
    [ReadOnly] public List<Upgrade> allUpgrades = new();
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
        return availableUpgrades[index];
    }


    [ContextMenu("Load Upgrades")]
    void LoadAllUpgrades()
    {
        if (!System.IO.Directory.Exists(upgradePath))
        {
            Debug.LogError("Upgrade path does not exist");
            return;
        }

        allUpgrades.Clear();

        string[] guids = 
             AssetDatabase.FindAssets("t:Upgrade", new[] { upgradePath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Upgrade upgrade = AssetDatabase.LoadAssetAtPath<Upgrade>(path);
            if (upgrade != null)
            {
                allUpgrades.Add(upgrade);
            }
        }
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
