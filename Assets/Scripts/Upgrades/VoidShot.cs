using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidShot : MonoBehaviour
{
    public float pullStrength;
    public float pullRadius;
    public float rotSpeed;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = rotSpeed;
    }


    void Update()
    {
           
    }
}
