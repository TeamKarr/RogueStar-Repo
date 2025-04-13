using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rb;
    AttributeManager attributes;
    public float maxSpeed = 10f;
    void Start()
    {
        attributes = GetComponent<AttributeManager>();
        //maxSpeed = attributes.getAttribute("MaxSpeed");
        

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

        rb.AddForce(transform.up * 2f * forward, ForceMode2D.Force);

    }
    void FixedUpdate()
    {
        //Debug.Log("Speed: " + rb.velocity.magnitude + " max: " + maxSpeed.getvalue());
        if (rb.velocity.magnitude > maxSpeed)
        {
            // Clamp the velocity magnitude
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
