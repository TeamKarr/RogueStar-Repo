using System.Collections;
using System.Collections.Generic;
// using System.Numerics;
using UnityEngine;

public class OrbitState : EnemyBrain.State
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    public float strength = 10f;
    public float orbitRadius = 3f;
    public float orbitSpeed = 2f; // in units per second along the orbit
    public float correctionForce = 10f; // force to keep enemy on orbit
    public float damping = 0.95f; // damp velocity error to avoid jitter


    public bool orientTowardsTarget = false;

    // Update is called once per frame
    public float roll = 0f;
    public float rollAmplifier;
    public int maxRoll;

    // public float GravStrength = 1;

    
    private float currentAngularVelocity = 0f;
    public float distanceToCreep=10f;

    public override void FixedAction(){
        Vector2 toPlayer = (Vector2)(Player.transform.position - transform.position);
        float currentDistance = toPlayer.magnitude;
        Vector2 directionToPlayer = toPlayer.normalized;

        // --- 1. Maintain Radius (Radial Correction) ---
        float radialError = currentDistance - orbitRadius;
        Vector2 radialCorrection = directionToPlayer * (radialError * correctionForce);
        rb.AddForce(radialCorrection);

        // --- 2. Desired Tangential Orbit Velocity ---
        Vector2 tangentDirection = Vector2.Perpendicular(directionToPlayer); // 90° from radius
        Vector2 desiredOrbitVelocity = tangentDirection * orbitSpeed;

        // --- 3. Add Player Velocity (if moving) ---
        if(currentDistance>distanceToCreep){
        Vector2 baseVelocity = Player.GetComponent<Rigidbody2D>() ? Player.GetComponent<Rigidbody2D>().velocity : Vector2.zero;
        Vector2 desiredVelocity = baseVelocity + desiredOrbitVelocity;
         Vector2 velocityError = desiredVelocity - rb.velocity;
        Vector2 velocityCorrection = velocityError * correctionForce * Time.fixedDeltaTime;
        rb.velocity += velocityCorrection * damping;
        }

        // --- 4. Velocity Correction ---
       
    }

    public override void Action()
    {

        // Vector2 direction = (Player.transform.position - transform.position).normalized;
        // if (orientTowardsTarget){
            
        //     this.transform.up = direction;
        // }
        // Vector2 tangent = new Vector2(-direction.y, direction.x).normalized;

        // rb.AddForce(orbitSpeed*orbitSpeed/orbitDistance * rb.mass * direction);
        // if(rb.velocity.magnitude<orbitSpeed){
        //     rb.AddForce(tangent*rb.mass);
        // }
        // if(rb.velocity.magnitude>orbitSpeed*Mathf.Sqrt((Player.transform.position - transform.position).magnitude)){
        //     rb.AddForce(rb.velocity.normalized*-1);
        // }
        
        // Vector2 directionOfFlight = new Vector2(rb.velocity.x,rb.velocity.y).normalized;

        // roll = Mathf.Clamp(currentAngularVelocity*Time.deltaTime * rollAmplifier, -1 * maxRoll, maxRoll);
        
        // Vector3 localRot = transform.localEulerAngles;
        // localRot.y = roll - 180;
        // this.transform.up = directionOfFlight;
        // transform.Rotate(Vector3.up, roll, Space.Self);
        // Debug.Log("roll:" + currentAngularVelocity);
        // currentAngularVelocity = Vector2.SignedAngle(transform.up, directionOfFlight);
        
        
         
    }
    public void tooClose(float strength)
    {
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        rb.AddForce(-direction*strength);
        Debug.Log("hit");
    }
    override public void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }
    
}
