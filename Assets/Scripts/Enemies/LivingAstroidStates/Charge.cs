using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charge : EnemyBrain.State
{
    private Rigidbody2D rb;

    public float maxSpeed = 10f;

    public float strikeAcceleration = 5f;

    private float lostTime = 0f;
    public float requiredEyeLostTime = 3f; // Set the required eye contact time in seconds

    public override void Action()
    {
        //Vector2 direction = (Player.transform.position - transform.position).normalized;
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        //// Check if the asteroid has a clear line of sight to the player
        //if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
        //{
        //    transform.up = direction;



        //}
        //else
        //{
        //    eyeContactTime = 0f;
        //}

        rb.AddForce(transform.up * strikeAcceleration, ForceMode2D.Force);

        if (rb.velocity.magnitude > maxSpeed)
        {
            // Clamp the velocity magnitude
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
