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
    public GameObject[] boosterObjects;
    private float boosterLevel = 0f;
    void Start()
    {
        foreach(GameObject booster in boosterObjects)
        {
            //booster.transform.localScale = new Vector3(boosterLevel, boosterLevel, boosterLevel);
            Debug.Log(booster.transform.localScale);
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

        if (forward > 0f)
        {
            boosterLevel += 0.1f;
        }
        else
        {
            boosterLevel =0f;
        }
        boosterLevel = Mathf.Min(boosterLevel, 1);

        boosterLevel = Mathf.Max(boosterLevel, 0);

        foreach (GameObject booster in boosterObjects)
        {
            booster.transform.localScale = new Vector3(boosterLevel, boosterLevel, boosterLevel);
        }

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
