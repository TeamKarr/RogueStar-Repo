using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent<float> OnGoldChange = new();


    // Game Values
    public int score = 0;
    public int Score
    {
        get { return matter; }
        set
        {
            matter = value;
            if (matter < 0)
            {
                matter = 0;
            }
            OnGoldChange.Invoke(matter);
        }
    }

    public int matter = 0;
    public int Matter
    {
        get { return matter; }
        set
        {
            matter = value;
            if (matter < 0)
            {
                matter = 0;
            }
            OnGoldChange.Invoke(matter);
        }
    }
    public void addMatter(int value)
    {
        Matter += value;
    }

    internal void addScore(int score)
    {
        Score += score;
    }

    // Visual Settings
    public float Bloom = 5f;

    // Player Upgrade
    [Serializable]
    public class AttributeUpgrade
    {
        public float amount;
        public float UpgradeAmount;
        public int MaxUpgrades;
        public int Cost;
        public int CostIncrease;
    }
    [Serializable]
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
    [SerializeField]
    public List<KeyValuePair<string, AttributeUpgrade>> defaultAttributesList = new List<KeyValuePair<string, AttributeUpgrade>>
       {
           new("MaxHealth", new AttributeUpgrade { amount = 100f, UpgradeAmount = 10f, MaxUpgrades = 7, Cost = 10, CostIncrease = 1}),
           new("MaxSpeed", new AttributeUpgrade { amount = 10f, UpgradeAmount = 1f, MaxUpgrades = 7, Cost = 10, CostIncrease = 1 }),
           new("Acceleration", new AttributeUpgrade { amount = 5f, UpgradeAmount = 0.5f, MaxUpgrades = 7, Cost = 10, CostIncrease = 1 }),
           new("RotationSpeed", new AttributeUpgrade { amount = 200f, UpgradeAmount = 20f, MaxUpgrades = 7, Cost = 10, CostIncrease = 1 }),
           new("Damage", new AttributeUpgrade { amount = 25f, UpgradeAmount = 5f, MaxUpgrades = 7, Cost = 10, CostIncrease = 1 }),
           new("FiringRate", new AttributeUpgrade { amount = 0.5f, UpgradeAmount = 0.5f, MaxUpgrades = 7, Cost = 10, CostIncrease = 1 }),
       };

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
        OnGoldChange.Invoke(matter);
    }

    private void InitializeAttributes()
    {
        defaultAttributes = new Dictionary<string, AttributeUpgrade>();
        foreach (var pair in defaultAttributesList)
        {
            defaultAttributes[pair.Key] = pair.Value;
        }
    }

    public void Start()
    {
        OnGoldChange.Invoke(matter);
    }

    

   


}
