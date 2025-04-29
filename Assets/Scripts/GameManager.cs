using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Player base attributes with max, upgrade amount, and max upgrades  

    [Header("Health Attributes")]
    public float MaxHealth = 100f;
    public float MaxHealthUpgradeAmount = 10f;
    public int MaxHealthMaxUpgrades = 7;

    [Header("Speed Attributes")]
    public float MaxSpeed = 10f;
    public float MaxSpeedUpgradeAmount = 1f;
    public int MaxSpeedMaxUpgrades = 7;

    [Header("Acceleration Attributes")]
    public float Acceleration = 5f;
    public float AccelerationUpgradeAmount = 0.5f;
    public int AccelerationMaxUpgrades = 7;

    [Header("Rotation Speed Attributes")]
    public float RotationSpeed = 200f;
    public float RotationSpeedUpgradeAmount = 20f;
    public int RotationSpeedMaxUpgrades = 7;

    [Header("Damage Attributes")]
    public float Damage = 25f;
    public float DamageUpgradeAmount = 5f;
    public int DamageMaxUpgrades = 7;

    [Header("Fire Rate Attributes")]
    public float FireRate = 0.5f;
    public float FireRateUpgradeAmount = -0.05f; // Negative to reduce fire rate  
    public int FireRateMaxUpgrades = 7;

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
    }
}
