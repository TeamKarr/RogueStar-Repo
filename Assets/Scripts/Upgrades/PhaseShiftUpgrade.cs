using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseShiftUpgrade : MonoBehaviour
{
    float cooldown = 1f;
    private float lastUsed = Mathf.NegativeInfinity;
    private float duration = 3f;
    private Rigidbody2D rb;

    private Collider2D coll;


    private PlayerMovement playerMovement;
    
    void Start()
    {
        Debug.Log("Short Burn Upgrade Started");
        coll = GetComponent<Collider2D>();
        //rb = GetComponent<Rigidbody2D>();
        //body = transform.Find("main ships");
        //playerMovement = GetComponent<PlayerMovement>();

        GetComponent<ShortBurnUpgrade>().afterDash.AddListener(StartShift);

    }

    public void StartShift()
    {
        Debug.Log("Phase Shift");
        StartCoroutine(Shift());
    }

    IEnumerator Shift()
    {   
        
           
        int originalLayer = gameObject.layer;

        // Disable collisions with all layers except border (layer 9)
        Physics2D.IgnoreLayerCollision(originalLayer, originalLayer, true);
        for (int i = 0; i < 32; i++)
        {
            if (i != 9) // Skip border layer
            {
            Physics2D.IgnoreLayerCollision(originalLayer, i, true);
            }
        }

        yield return new WaitForSeconds(duration);

        // Restore collisions with all layers
        for (int i = 0; i < 32; i++)
        {
            if (i != 9) // Skip border layer
            {
            Physics2D.IgnoreLayerCollision(originalLayer, i, false);
            }
        }
        Physics2D.IgnoreLayerCollision(originalLayer, originalLayer, false);


        
    }
}
