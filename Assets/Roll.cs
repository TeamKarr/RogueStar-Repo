using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    private float roll = 0f;
    private Transform body;
    public bool correctRotation = false;
    public float rollAmplifier;
    public float aungularAcceleration;
    public float maxRoll;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
         GetComponent<ShipParts>().initialize();
        body=GetComponent<ShipParts>().ship.transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        roll =Mathf.Clamp(rb.angularVelocity/aungularAcceleration*rollAmplifier,-1*maxRoll,maxRoll);
        
        Vector3 localRot = body.transform.localEulerAngles;
        if (correctRotation)
            localRot.y = roll-180;
        else
            localRot.y = roll;
        body.localEulerAngles = localRot;
        
    }
}
