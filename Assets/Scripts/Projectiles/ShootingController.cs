using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Unity.Mathematics;
using UnityEngine;

/// <summary>
/// A class which controlls player aiming and shooting
/// </summary>
public class ShootingController : MonoBehaviour
{
    
    
    
    
    [Tooltip("Whether this shooting controller is controled by the player")]
    public bool isPlayerControlled = false;

    public bool shootTowardsPlayer = false;
    private GameObject player;

    [Header("Firing Settings")]
    [Tooltip("The minimum time between projectiles being fired.")]
    // public float fireRate = 0.05f;

    private Attribute fireRate;
    public string fireRateAttribute = "FiringRate";

    [Tooltip("The maximum diference between the direction the" +
        " shooting controller is facing and the direction projectiles are launched.")]
    public float projectileSpread = 1.0f;

    // The last time this component was fired
    private float lastFired = Mathf.NegativeInfinity;

    [Header("Effects")]
    [Tooltip("The effect to create when this fires")]
    public GameObject fireEffect;

    [Header("Bullet Spawnpoints")]
    public Transform[] spawnpoints;
    [Header("GameObject/Component References same order as spawn point")]
    [Tooltip("The projectile to be fired.")]
    public List<GameObject> projectilePrefabs;

    /// <summary>
    /// Description:
    /// Standard unity function that runs every frame
    /// Inputs:
    /// none
    /// Returns:
    /// void (no return)
    /// </summary>
    private void Update()
    {
        ProcessInput();
    }

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fireRate = GetComponent<AttributeManager>().getAttribute(fireRateAttribute);
    }

    private void LateUpdate()
    {
        if (!isPlayerControlled)
        {
            Fire();
        }
    }

    /// <summary>
    /// Description:
    /// Reads input from the input manager
    /// Inputs:
    /// None
    /// Returns:
    /// void (no return)
    /// </summary>
    void ProcessInput()
    {
        if (isPlayerControlled)
        {
            if (Input.GetMouseButton(0) == true)
            {
                Fire();
            }
        }
    }

    /// <summary>
    /// Description:
    /// Fires a projectile if possible
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void Fire()
    {
        float variance = 0f;
        // If the cooldown is over fire a projectile
        
            if (!isPlayerControlled)
            {
                variance = UnityEngine.Random.Range(-0.5f, 0.5f);
            }
            if ((Time.timeSinceLevelLoad - lastFired) > fireRate.getvalue() + variance)
            {
                // Launches a projectile
                for (int i = 0; i < spawnpoints.Length; i++)
                {
                    if (projectilePrefabs[i] != null)
                        SpawnProjectile(projectilePrefabs[i],spawnpoints[i]);
                }
                if (fireEffect != null)
                {

                    Instantiate(fireEffect, transform.position, transform.rotation, null);
                }

                // Restart the cooldown
                lastFired = Time.timeSinceLevelLoad;
            }
        
    }

    /// <summary>
    /// Description:
    /// Spawns a projectile and sets it up
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    public void SpawnProjectile(GameObject _projectilePrefab, Transform _spawnPoint)
    {

        // Check that the prefab is valid
        if (_projectilePrefab != null)
        {
            Vector3 pos = _spawnPoint.position;
            // Create the projectile
            Quaternion rotation;
            GameObject projectileGameObject;
            if (shootTowardsPlayer)
            {
                Vector2 direction = player.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rotation = Quaternion.Euler(0f, 0f, angle-90);
                projectileGameObject = Instantiate(_projectilePrefab, pos, rotation, null);
            }
            else
            {
                projectileGameObject = Instantiate(_projectilePrefab, pos, _spawnPoint.rotation, null);
            }
            
            projectileGameObject.GetComponent<Projectile>().Fired = gameObject;
            // Account for spread
            Vector3 rotationEulerAngles = projectileGameObject.transform.rotation.eulerAngles;
            rotationEulerAngles.z += UnityEngine.Random.Range(-projectileSpread, projectileSpread);
            projectileGameObject.transform.rotation = Quaternion.Euler(rotationEulerAngles);

            // Keep the heirarchy organized
           
        }
    }
}
