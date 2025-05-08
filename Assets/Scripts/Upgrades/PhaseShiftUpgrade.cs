using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseShiftUpgrade : MonoBehaviour
{
    float cooldown = 1f;
    private float lastUsed = Mathf.NegativeInfinity;
    private float duration = 10f;
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
        {
            coll.enabled = false;
           

            yield return new WaitForSeconds(duration);

            //coll.enabled = true;

        }
    }
}
