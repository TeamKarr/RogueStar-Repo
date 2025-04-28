using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RocketMenuMovement : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 targetPosition;
    private Rigidbody rb;

    public float speed = 1.0f;
    public float acceleration = 0.5f;
    public float maxSpeed = 5.0f;
    internal Func<int> OnRocketDestroyed;

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
        float toTarget = Vector3.Distance(startPosition, targetPosition);
        float toCurrent = Vector3.Distance(startPosition, transform.position);
        if (toTarget < toCurrent)
        {
            Destroy(gameObject);
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
