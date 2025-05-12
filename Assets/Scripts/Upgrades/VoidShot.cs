using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidShot : MonoBehaviour
{
    private Rigidbody2D rb;

    public float pullStrength;
    public float pullRadius;
    public int cooldown;

    private Projectile projectileSpeed;
    [HideInInspector] public GameObject Fired;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed.bulletSpeed.getvalue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
