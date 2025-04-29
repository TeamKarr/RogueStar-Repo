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
    Attribute rotationSpeed;

    [Header("Attribute Reference")]
    public string maxSpeedAttribute = "MaxSpeed";
    public string accelerationAttribute = "Acceleration";
    public string rotationAttribute = "RotationSpeed";

    [Header("Movement Visuals")]
    public GameObject[] boosterObjects;
    private float boosterLevel = 0f;



    public ParticleSystem smoke;
    public Light fireLight;
    private float lightIntensity = 1f;

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
        rotationSpeed = (GetComponent<AttributeManager>()).getAttribute(rotationAttribute);
        lightIntensity = fireLight.intensity;
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
        // update visual of the ship

        float forward = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // Apply roll to the body
        roll =Mathf.Clamp(rb.angularVelocity/rotationSpeed.getvalue()*8,-1*maxRoll,maxRoll);
        
        Vector3 localRot = body.transform.localEulerAngles;
        localRot.y = roll-180;
        body.localEulerAngles = localRot;

        // Handle booster effects

        //switch (forward)
        //{
        //    case 1f:
        //        if (transform.rotation == 90f)
        //        {
        //            BoosterEffects();
        //        }
        //        break;
        //    default:

        //        break;
        //}
        BoosterEffects();


    }
    void FixedUpdate()
    {
        // Manage physics and movement of the ship
        float forward = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        // Move ship forward
        //rb.AddForce(transform.up * acceleration.getvalue() * forward, ForceMode2D.Force);
        rb.AddForce(new Vector3(0, acceleration.getvalue() * forward), ForceMode2D.Force);

        // Allow for straffing
        //rb.AddForce(horizontal * transform.right * acceleration.getvalue() * 0.75f, ForceMode2D.Force);
        rb.AddForce(new Vector3(horizontal * acceleration.getvalue(), 0), ForceMode2D.Force);

        // Clamp the velocity magnitude
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed.getvalue());

        // Rotate ship towards the mouse
        Vector2 m = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        Vector2 direction = (m - (Vector2) transform.position).normalized;

        Quaternion rotation = Quaternion.AngleAxis(90, transform.forward);
        direction= rotate(direction, -Mathf.PI/2);
        rb.AddTorque( Vector2.Dot(transform.up, direction)*rotationSpeed.getvalue());
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
    public void BoosterEffects()
    {
        float forward = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        if (forward > 0f)
        {
            boosterLevel += 0.05f;
            fireLight.intensity += 0.2f;
            if (smoke.isPlaying == false)
                smoke.Play();
        }
        else
        {
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
    }
}
