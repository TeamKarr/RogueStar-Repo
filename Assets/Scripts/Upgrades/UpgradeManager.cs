using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

public class UpgradeManager : MonoBehaviour
{
    
    [ReadOnly] public List<Upgrade> activeUpgrades = new();

    public string upgradePath = "Assets/Upgrades";
    [ReadOnly] public List<Upgrade> loadedUpgrades = new();
    [ReadOnly] public List<Upgrade> availableUpgrades;

    private List<float> culmitiveWeights = new();

    private AttributeManager attributeManager;

    public void addUpgrade(Upgrade upgrade)
    {
        activeUpgrades.Add(upgrade);
        upgrade.Initialise(gameObject);
    }

    public void removeUpgrade(Upgrade upgrade)
    {
        activeUpgrades.Remove(upgrade);   
    }

    public (Upgrade, Upgrade, Upgrade) GetUpgradeChoices()
    {
        var upgrade1 = getRandomUpgrade();
        var upgrade2 = getRandomUpgrade();
        var upgrade3 = getRandomUpgrade();
        return (upgrade1, upgrade2, upgrade3);
    }

    Upgrade getRandomUpgrade(){
        // pick a random upgrade from the available upgrades using the float in the tuple as the weight:
        var value = Random.Range(0,culmitiveWeights.Last());
        int index = culmitiveWeights.BinarySearch(value);
        if (index < 0)
            index = ~index;
        //Debug.Log(index);
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

        loadedUpgrades.Clear();

        string[] guids = 
             AssetDatabase.FindAssets("t:Upgrade", new[] { upgradePath });
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Upgrade upgrade = AssetDatabase.LoadAssetAtPath<Upgrade>(path);
            if (upgrade != null)
            {
                loadedUpgrades.Add(upgrade);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        culmitiveWeights.Add(0);
        attributeManager = GetComponent<AttributeManager>();
        
        foreach (var upgrade in loadedUpgrades)
        {
            availableUpgrades.Add(upgrade);
            culmitiveWeights.Add(culmitiveWeights.Last()+upgrade.weight);
        }
        culmitiveWeights.RemoveAt(0);
        //Debug.Log(culmitiveWeights);
    }
}
