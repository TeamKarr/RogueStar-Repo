using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMenuMovement : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 targetPosition;
    private Rigidbody rb;

    public float speed = 1.0f;
    public float acceleration = 0.5f;
    public float maxSpeed = 5.0f;

    void Start()
    {
        startPosition = transform.position;

        // Make the ship look at the target position  
        Vector3 directionToTarget = (targetPosition - startPosition).normalized;
        transform.rotation = Quaternion.LookRotation(directionToTarget);

        rb = GetComponent<Rigidbody>();

        // Set velocity to move where it is facing  
        rb.velocity = transform.forward * speed;
    }

    public void StartMove()
    {
        Start();
    }

    void Update()
    {
        Vector3 toTarget = targetPosition - startPosition;
        Vector3 toCurrent = transform.position - startPosition;

        if (Vector3.Dot(toTarget.normalized, toCurrent) > 1.0f)
        {
            Destroy(this);
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * acceleration, ForceMode.Force);
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}
