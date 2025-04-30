using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public class AttributeUpgrade
    {
        public float amount;
        public float UpgradeAmount;
        public int MaxUpgrades;
    }

    public class KeyValuePair<T, V>
    {
        public T Key;
        public V Value;
        public KeyValuePair(T key, V value)
        {
            Key = key;
            Value = value;
        }
    }

    [HideInInspector] public Dictionary<string, AttributeUpgrade> defaultAttributes;

    public List<KeyValuePair<string, AttributeUpgrade>> defaultAttributesList = new List<KeyValuePair<string, AttributeUpgrade>>
       {
           new("MaxHealth", new AttributeUpgrade { amount = 100f, UpgradeAmount = 10f, MaxUpgrades = 7 }),
           new("MaxSpeed", new AttributeUpgrade { amount = 10f, UpgradeAmount = 1f, MaxUpgrades = 7 }),
           new("Acceleration", new AttributeUpgrade { amount = 5f, UpgradeAmount = 0.5f, MaxUpgrades = 7 }),
           new("RotationSpeed", new AttributeUpgrade { amount = 200f, UpgradeAmount = 20f, MaxUpgrades = 7 }),
           new("Damage", new AttributeUpgrade { amount = 25f, UpgradeAmount = 5f, MaxUpgrades = 7 }),
           new("FireRate", new AttributeUpgrade { amount = 0.5f, UpgradeAmount = 0.05f, MaxUpgrades = 7 }),
       };

    public float Bloom = 5f;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeAttributes();
    }

    private void InitializeAttributes()
    {
        defaultAttributes = new Dictionary<string, AttributeUpgrade>();
        foreach (var pair in defaultAttributesList)
        {
            defaultAttributes[pair.Key] = pair.Value;
        }
    }

}
