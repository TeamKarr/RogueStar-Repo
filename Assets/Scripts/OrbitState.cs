using System.Collections;
using System.Collections.Generic;
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
    public float roll = 0f;
    public float rollAmplifier;
    public int maxRoll;

    
    private float currentAngularVelocity = 0f;
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
        
        Vector2 directionOfFlight = new Vector2(rb.velocity.x,rb.velocity.y).normalized;

        roll = Mathf.Clamp(currentAngularVelocity*Time.deltaTime * rollAmplifier, -1 * maxRoll, maxRoll);
        
        Vector3 localRot = transform.localEulerAngles;
        localRot.y = roll - 180;
        this.transform.up = directionOfFlight;
        transform.Rotate(Vector3.up, roll, Space.Self);
        Debug.Log("roll:" + currentAngularVelocity);
        currentAngularVelocity = Vector2.SignedAngle(transform.up, directionOfFlight);
        
        
         
    }
    public void tooClose(float strength)
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        rb.AddForce(-direction*strength);
        Debug.Log("hit");
    }
     void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    
}
