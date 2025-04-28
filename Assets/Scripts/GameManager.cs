using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Player base attributes  
    public float MaxHealth = 100f;
    public float MaxSpeed = 10f;
    public float Acceleration = 5f;
    public float RotationSpeed = 200f;
    public float Damage = 25f;
    public float FireRate = 0.5f;

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
