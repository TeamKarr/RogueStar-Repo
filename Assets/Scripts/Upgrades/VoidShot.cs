using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoidShot : MonoBehaviour
{
    public float pullStrength;
    public float rotSpeed;
    private Rigidbody2D rb, collidedrb;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularVelocity = rotSpeed;
    }


    void Update()
    {
         
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger Entered");
        Vector2 direction = (this.transform.position - collision.transform.position).normalized;
        collidedrb = collision.GetComponent<Rigidbody2D>();
        collidedrb.AddForce(direction * pullStrength, ForceMode2D.Force);
    }

}
