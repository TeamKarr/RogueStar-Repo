using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VoidShot : MonoBehaviour
{
    public float pullStrength;
    public float rotSpeed;
    private Rigidbody2D rb, collidedrb;
    Collider2D pullcollider;
    Collider2D[] hitColliders;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pullcollider = rb.GetComponents<Collider2D>()[1];
        rb.angularVelocity = rotSpeed;
    }


    void FixedUpdate()
    {
        hitColliders = Physics2D.OverlapCircleAll(transform.position, 5f);
        if (hitColliders != null)
        {
            foreach (Collider2D collider in hitColliders)
            {
                if (collider != null && collider.gameObject.layer == 7)
                {
                    Vector2 direction = (this.transform.position - collider.gameObject.transform.position).normalized;
                    collidedrb = collider.gameObject.GetComponent<Rigidbody2D>();
                    collidedrb.AddForce(direction * pullStrength, ForceMode2D.Force);
                }
            }
        }
    }
}
    
