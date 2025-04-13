using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : EnemyBrain.State
{
    private Rigidbody2D rb;
    public float speedAcceleration = 2f;
    public float maxSpeed = 4f;

    public override void Action()
    {
        // get pos of enemy and pos of player and 
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        this.transform.up = direction;

        rb.AddForce(transform.up * speedAcceleration, ForceMode2D.Force);

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
