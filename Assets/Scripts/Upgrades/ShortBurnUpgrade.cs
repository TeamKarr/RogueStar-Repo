using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortBurnUpgrade : MonoBehaviour
{
    float cooldown = 1f;
    private float lastUsed = Mathf.NegativeInfinity;
    private float speed = 50f;
    private Rigidbody2D rb;
    private Transform body;
    
    void Start()
    {
        Debug.Log("Short Burn Upgrade Started");
        rb = GetComponent<Rigidbody2D>();
        body = transform.Find("main ships");
    }



    void Update()
    {
        if (Input.GetKeyDown("left shift"))
        {
            Debug.Log("Short Burn");
            if ((Time.timeSinceLevelLoad - lastUsed) > cooldown)
                StartCoroutine(shortBurn());


        }
    }

    IEnumerator shortBurn()
    {
        
        {
            Debug.Log("Time passed");
            this.GetComponent<PlayerMovement>().enabled = false;
            Vector2 startVelo = rb.velocity;
            rb.velocity = transform.up * speed;
            //Debug.Log(rb.velocity);
            float rolls = 2*360; // 2 rotations
            Vector3 localRot = body.transform.localEulerAngles;
            float roll = localRot.y;
            float elapsedTime = 0f;

            float totalTime = 0.25f;

            //while (elapsedTime < totalTime)
            //{
            //    localRot = body.transform.localEulerAngles;
            //    roll += rolls / totalTime;
            //    localRot.y = roll;
            //    body.localEulerAngles = localRot;
            //    totalTime += Time.deltaTime;
            //}

            //shipModel.transform.Rotate


            //rb.velocity = startVelo;
            // Restart the cooldown
            lastUsed = Time.timeSinceLevelLoad;
            this.GetComponent<PlayerMovement>().enabled = enabled;
            yield return null;

        }
    }
}
