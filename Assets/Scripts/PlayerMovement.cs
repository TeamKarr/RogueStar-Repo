using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

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

    void Start()
    {

        foreach(GameObject booster in boosterObjects)
        {
            booster.transform.localScale = new Vector3(booster.transform.localScale.x, boosterLevel, booster.transform.localScale.z);
        }
        
        maxSpeed = (GetComponent<AttributeManager>()).getAttribute(maxSpeedAttribute);
        acceleration = (GetComponent<AttributeManager>()).getAttribute(accelerationAttribute);

        if (maxSpeed == null)
        {
            Debug.LogError("Max Speed Attribute not found");
        }
        if (acceleration == null)
        {
            Debug.LogError("Acceleration Attribute not found");
        }
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found");
        }
    }
    // Update is called once per frame
    void Update()
    {

        // rotate ship
        Vector2 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.Rotate(new Vector3(0,0,1), 1);
        Vector2 direction = (m - (Vector2) transform.position ).normalized;
        transform.up = direction;

        // move ship
        float forward = Input.GetAxis("Vertical");
        //Debug.Log("Forward: " + forward);
        if (forward > 0f)
        {
            boosterLevel += 0.05f;
            if (smoke.isPlaying == false)
                smoke.Play();
            

        }
        else
        {
            boosterLevel -= 0.03f;
            if (smoke.isPlaying)
                smoke.Stop();
      
        }

        boosterLevel = Mathf.Clamp(boosterLevel, 0, 1);

        foreach (GameObject booster in boosterObjects)
        {
             booster.transform.localScale = new Vector3(booster.transform.localScale.x, boosterLevel, booster.transform.localScale.z);
        }

        rb.AddForce(transform.up * acceleration.getvalue() * forward, ForceMode2D.Force);

    }
    void FixedUpdate()
    {
        //Debug.Log("Speed: " + rb.velocity.magnitude + " max: " + maxSpeed.getvalue());
        if (rb.velocity.magnitude > maxSpeed.getvalue())
        {
            // Clamp the velocity magnitude
            rb.velocity = rb.velocity.normalized * maxSpeed.getvalue();
        }
    }
}
