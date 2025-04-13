using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class OnCollisionTrigger : MonoBehaviour
{
    // Start is called before the first frame update

    public bool useRadius = true;

    [ConditionalProperty("useRadius")]
    public float radius = 1.0f;
    [ConditionalProperty("useRadius")]
    public Transform player;

    private Collision2D collision;

    [SerializeField]
    public delegate void Delagate();

    public UnityEvent onCollisionEnter;

    public UnityEvent onCollisionExit;

    private bool entered = false;

    void Start()
    {
        if (!useRadius)
        {
            collision = GetComponent<Collision2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (useRadius)
        {
            //Debug.Log(Vector2.Distance(this.transform.position, player.position) > radius);
            if (entered)
            {
                if (Vector2.Distance(this.transform.position, player.position) > radius)
                {
                    runOnExit();
                    entered = false;
                }
            }
            else
            {
                if (Vector2.Distance(this.transform.position, player.position) <= radius)
                {
                    runOnEnter();
                    entered = true;
                }
            }
            

        }
    }

    public void runOnEnter()
    {
        onCollisionEnter.Invoke();
        
    }

    public void runOnExit()
    {
        onCollisionExit.Invoke();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!useRadius)
        {
            if (player.transform.Equals(collision.gameObject.transform))
            {
                entered = true;
                Debug.Log("Object entered trigger: " + collision.gameObject.name);
                runOnEnter();
            }
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!useRadius)
        {
            if (player.transform.Equals(collision.gameObject.transform))
            {
                entered = false;
                Debug.Log("Object entered trigger: " + collision.gameObject.name);
                runOnExit();
            }
        }

    }


}
