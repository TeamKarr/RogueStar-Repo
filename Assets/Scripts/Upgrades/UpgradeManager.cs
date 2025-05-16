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
        availableUpgrades = loadedUpgrades.Where(
            x => !activeUpgrades.Contains(x) && (x.requirements.Length == 0 || x.requirements.All(a => activeUpgrades.Contains(a))) && 
            (x.conflicts.Length == 0 || !x.conflicts.Any(a => activeUpgrades.Contains(a)))).ToList();
        culmitiveWeights.Clear();
    }

    public void removeUpgrade(Upgrade upgrade)
    {
        activeUpgrades.Remove(upgrade);   
    }

    public (Upgrade, Upgrade, Upgrade) GetUpgradeChoices()
    {

        var upgrades = pickRandom<Upgrade>(availableUpgrades, availableUpgrades.Select(a => a.weight).ToList(), 3);

        return (upgrades[0], upgrades[1], upgrades[2]);
    }

    Upgrade getRandomUpgrade(){

        //if (culmitiveWeights.Count == 0)
        //{
        //    GenerateCulmitiveWeights();
        //}

        // pick a random upgrade from the available upgrades using the float in the tuple as the weight:
        var value = Random.Range(0,culmitiveWeights.Last());
        int index = culmitiveWeights.BinarySearch(value);
        if (index < 0)
            index = ~index;
        //Debug.Log(index);

        // refresh available upgrades
        Debug.Log(culmitiveWeights);
        Debug.Log(index);
        return availableUpgrades[index];
    }

    //private void GenerateCulmitiveWeights()
    //{
    //    //culmitiveWeights.Clear();
    //    //culmitiveWeights.Add(0);
    //    foreach (var upgrade in availableUpgrades)
    //    {
    //        culmitiveWeights.Add(culmitiveWeights.LastOrDefault()+upgrade.weight);
    //    }
        
    //}

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
        //culmitiveWeights = new();
        attributeManager = GetComponent<AttributeManager>();
        
        foreach (var upgrade in loadedUpgrades)
        {
            if (upgrade.requirements.Length == 0)
                availableUpgrades.Add(upgrade);
        }
        // culmitiveWeights.RemoveAt(0);
        //Debug.Log(culmitiveWeights);
    }


    public static List<T> pickRandom<T>(List<T> list, List<float> weights, int count = 1) where T : class
    {
        List<T> selectedItems = new();
        List<T> copy = new(list);
        List<float> weightCopy = new(weights); 

        for (int i = 0; i < count; i++)
        {
            if (copy.Count == 0)
            {
                selectedItems.Add(null);
            }
            float totalWeight = weightCopy.Sum();
            float randomValue = Random.Range(0f, totalWeight);

            float currentWeight = 0f;
            for (int j = 0; j < copy.Count; j++)
            {
                currentWeight += weightCopy[j];
                if (randomValue <= currentWeight)
                {
                    selectedItems.Add(copy[j]);
                    copy.RemoveAt(j);
                    weightCopy.RemoveAt(j);
                    break;
                }
            }
        }

        return selectedItems;
    } 
}

