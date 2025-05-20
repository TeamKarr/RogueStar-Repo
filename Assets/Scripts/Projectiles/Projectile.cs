using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to make projectiles move
/// </summary>
public class Projectile : MonoBehaviour
{
    
    private Rigidbody2D rb;

    public string bulletSpeedAttribute = "Bullet Speed";
    [HideInInspector]public Attribute bulletSpeed;

    [HideInInspector] public GameObject Fired;

    void Start()
    {
        bulletSpeed = Fired.GetComponent<AttributeManager>().getAttribute(bulletSpeedAttribute);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up* bulletSpeed.getvalue();

    }
        
    /// <summary>
    /// Description:
    /// Move the projectile in the direction it is heading
    /// Inputs: 
    /// none
    /// Returns: 
    /// void (no return)
    /// </summary>
    
}