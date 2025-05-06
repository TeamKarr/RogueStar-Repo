using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortBurnUpgrade : MonoBehaviour
{
    float cooldown = 1f;
    private float lastUsed = Mathf.NegativeInfinity;
    private float speed = 10f;
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKeyDown("left shift") == true)
        {
            
            shortBurn();
        }
    }

    public void shortBurn()
    {
        if ((Time.timeSinceLevelLoad - lastUsed) > cooldown)
        {   
            this.GetComponent<PlayerMovement>().enabled = false;
            Vector2 startVelo = rb.velocity;
            rb.velocity = transform.up * speed;
            Debug.Log(rb.velocity);
            var thing = new WaitForSeconds(5f);
            rb.velocity = startVelo;
            // Restart the cooldown
            lastUsed = Time.timeSinceLevelLoad;
        }
        this.GetComponent<PlayerMovement>().enabled = enabled;
    }
}
