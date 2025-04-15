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

    public float localmaxSpeed = 1f;

    public float distanceToMouseLimit = 400;
    public GameObject[] boosterObjects;
    private float boosterLevel = 0f;



    public ParticleSystem smoke;
    void Start()
    {

        foreach(GameObject booster in boosterObjects)
        {
            
            booster.transform.localScale = new Vector3(booster.transform.localScale.x, boosterLevel, booster.transform.localScale.z);

            
        }
        
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
        //Debug.Log("Forward: " + forward);
        if (forward > 0f)
        {
            boosterLevel += 0.05f;
            smoke.Play();

        }
        else
        {
            boosterLevel -= 0.1f;
            smoke.Stop();
        }
        boosterLevel = Mathf.Min(boosterLevel, 1);

        boosterLevel = Mathf.Max(boosterLevel, 0);

        foreach (GameObject booster in boosterObjects)
        {
            booster.transform.localScale = new Vector3(boosterLevel, boosterLevel, boosterLevel);
        }
        Vector3 objectSreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPos = Input.mousePosition;
        float distanceToMouse = Vector3.Distance(objectSreenPos, mouseScreenPos);
        Debug.Log("Distance to mouse: " + distanceToMouse);
        localmaxSpeed = Mathf.Min(distanceToMouse/distanceToMouseLimit,1)*maxSpeed;
        //Debug.Log("Local max speed: " + localmaxSpeed);
        //Debug.Log("max speed: " + maxSpeed);
        rb.AddForce(transform.up * 2f * forward, ForceMode2D.Force);

    }
    void FixedUpdate()
    {
        //Debug.Log("Speed: " + rb.velocity.magnitude + " max: " + maxSpeed.getvalue());
        if (rb.velocity.magnitude > localmaxSpeed)
        {
            // Clamp the velocity magnitude
            rb.velocity = rb.velocity.normalized * localmaxSpeed;
        }
    }
}
