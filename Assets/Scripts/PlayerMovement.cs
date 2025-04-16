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



    public ParticleSystem smoke;
    public Light fireLight;
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
        fireLight.intensity = Mathf.Min(fireLight.intensity, 2.5f);

        fireLight.intensity = Mathf.Max(fireLight.intensity, 0);

        boosterLevel = Mathf.Min(boosterLevel, 1);

        boosterLevel = Mathf.Max(boosterLevel, 0);

        foreach (GameObject booster in boosterObjects)
        {
  booster.transform.localScale = new Vector3(booster.transform.localScale.x, boosterLevel, booster.transform.localScale.z);
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
