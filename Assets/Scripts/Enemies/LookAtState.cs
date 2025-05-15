using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : EnemyBrain.State
{
    private Rigidbody2D rb;
    public float angularAcceleration = 0.5f;
    public override void Action()
    {
        // get pos of enemy and pos of player and 
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        Vector2 currentDirection = transform.up;
        float angle = Vector2.SignedAngle(currentDirection, direction);
        rb.AddTorque(angle * angularAcceleration);
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
