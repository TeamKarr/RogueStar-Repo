using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalk : EnemyBrain.State
{
    private float eyeContactTime = 0f;
    public float requiredEyeContactTime = 3f; // Set the required eye contact time in seconds

    private Physics2D physics;

    private EnemyBrain brain;

    public override void Action()
    {
        // look at enemy
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        Debug.Log("raycast hit: " + hit.collider);
        // Check if the asteroid has a clear line of sight to the player
        if (hit.collider != null && hit.collider.gameObject.Equals(Player))
        {
            transform.up = direction;


            eyeContactTime += Time.deltaTime;
            if (eyeContactTime >= requiredEyeContactTime)
            {
                brain.setState<Charge>();
            }
            Debug.Log("eye contact time: " + eyeContactTime + " seconds");
        }
        else
        {
            eyeContactTime = 0f;
            Debug.Log("lost eye contact");
        }
    }

    void Start()
    {
        brain = GetComponent<EnemyBrain>();
        physics = GetComponent<Physics2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
