using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : EnemyBrain.State
{
    public override void Action()
    {
        // get pos of enemy and pos of player and 
        Vector2 direction = (Player.transform.position - transform.position).normalized;
        transform.up = direction;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
