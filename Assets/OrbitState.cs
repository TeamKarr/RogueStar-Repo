using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OrbitState : EnemyBrain.State
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float orbitSpeed = 2f;
    public float forceMultiplier = 4f;

    public float orbitDistance;

    public bool orientTowardsTarget = false;

    // Update is called once per frame
    
    public override void Action()
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        if (orientTowardsTarget){
            
            this.transform.up = direction;
        }
        Vector2 tangent = new Vector2(-direction.y, direction.x).normalized;

        rb.AddForce(orbitSpeed*orbitSpeed/orbitDistance * rb.mass * direction);
        if(rb.velocity.magnitude<orbitSpeed){
            rb.AddForce(tangent*rb.mass);
        }
        if(rb.velocity.magnitude>orbitSpeed*Mathf.Sqrt((Player.transform.position - transform.position).magnitude)){
            rb.AddForce(rb.velocity.normalized*-1);
        }
        
        
    }
     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    
}
