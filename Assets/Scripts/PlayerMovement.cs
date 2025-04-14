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

    void Start()
    {
        maxSpeed = (GetComponent<AttributeManager>()).getAttribute(maxSpeedAttribute);
        acceleration = (GetComponent<AttributeManager>()).getAttribute(accelerationAttribute);

        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {

        // rotate ship
        Vector2 m = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        transform.Rotate(new Vector3(0,0,1), 1);
        Vector2 direction = (m - (Vector2) transform.position ).normalized;
        transform.up = direction;

        // move ship
        float forward = Input.GetAxis("Vertical");

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
