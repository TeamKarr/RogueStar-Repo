using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    Attribute maxSpeed;
    Attribute acceleration;

    public string maxSpeedAttribute = "MaxSpeed";
    public string accelerationAttribute = "Acceleration";

    public GameObject[] boosterObjects;
    private float boosterLevel = 0f;



    public ParticleSystem smoke;
    public Light fireLight;
    private float lightIntensity = 1f;
    public float AngularAcceleration=1f;
    private float roll = 0f;

    public float maxRoll = 90f;
    public Transform body;

    void Start()
    {

        foreach(GameObject booster in boosterObjects)
        {
            booster.transform.localScale = new Vector3(booster.transform.localScale.x, boosterLevel, booster.transform.localScale.z);
        }
        Debug.Log(GetComponent<AttributeManager>());
        maxSpeed = (GetComponent<AttributeManager>()).getAttribute(maxSpeedAttribute);
        acceleration = (GetComponent<AttributeManager>()).getAttribute(accelerationAttribute);
        lightIntensity = fireLight.intensity;
        if (maxSpeed == null)
        {
            Debug.LogError("Max Speed Attribute not found");
        }
        if (acceleration == null)
        {
            Debug.LogError("Acceleration Attribute not found");
        }
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb);
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found");
        }
    }
    // Update is called once per frame
    void Update()
    {

        // rotate ship

        
        
        //transform.up = direction;

        // move ship
        float forward = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        roll =Mathf.Clamp(rb.angularVelocity/AngularAcceleration*8,-1*maxRoll,maxRoll);
        
        Vector3 localRot = body.transform.localEulerAngles;
        localRot.y = roll-180;
        body.localEulerAngles = localRot;
        //Debug.Log("Forward: " + forward);
        if (forward > 0f)
        {
            
            boosterLevel += 0.05f;
            fireLight.intensity += 0.2f;
            if (smoke.isPlaying == false)
                smoke.Play();
            

        }
        else
        {
            // does stuff
            fireLight.intensity -= 0.2f;
            boosterLevel -= 0.05f;
            if (smoke.isPlaying)
                smoke.Stop();
      
        }
        fireLight.intensity = Mathf.Min(fireLight.intensity, lightIntensity);

        fireLight.intensity = Mathf.Max(fireLight.intensity, 0);

        boosterLevel = Mathf.Min(boosterLevel, 1);

        boosterLevel = Mathf.Clamp(boosterLevel, 0, 1);

        foreach (GameObject booster in boosterObjects)
        {
             booster.transform.localScale = new Vector3(booster.transform.localScale.x, boosterLevel, booster.transform.localScale.z);
        }

        rb.AddForce(transform.up * acceleration.getvalue() * forward, ForceMode2D.Force);
        if (body.rotation.z > 135 || body.rotation.z < -135)
        {
            horizontal *= -1;
        }
        rb.AddForce(new Vector2(horizontal * acceleration.getvalue(), 0), ForceMode2D.Force);
    }
    void FixedUpdate()
    {
        //Debug.Log("Speed: " + rb.velocity.magnitude + " max: " + maxSpeed.getvalue());
        if (rb.velocity.magnitude > maxSpeed.getvalue())
        {
            // Clamp the velocity magnitude
            rb.velocity = rb.velocity.normalized * maxSpeed.getvalue();
        }
         Vector2 m = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        Vector2 direction = (m - (Vector2) transform.position).normalized;

        Quaternion rotation = Quaternion.AngleAxis(90, transform.forward);
        direction= rotate(direction, -Mathf.PI/2);
        rb.AddTorque( Vector2.Dot(transform.up, direction)*AngularAcceleration);
    }
    public static Vector2 rotate(Vector2 v, float delta) {
    return new Vector2(
        v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
        v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
    );
}

    public void goToHome()
    {
        this.transform.position = new Vector3(0, 0, transform.position.z);
    }
}
