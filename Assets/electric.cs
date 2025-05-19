using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electric : MonoBehaviour
{
    private float lastFired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.timeSinceLevelLoad - lastFired) > 0.1 + Random.Range(0.0f, 0.05f))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            lastFired = Time.timeSinceLevelLoad;
            
        }
    }
}
