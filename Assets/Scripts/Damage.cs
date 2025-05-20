using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Tooltip("Whether to destroy child gameobjects when this gameobject is destroyed")]
    public bool destroyChildrenOnDeath = true;

    public string damgageAttribute = "Damage";
    Attribute damage;

    public string pierceAttribute = "Piercing";
    Attribute pierce;
    private int collided = 0;

    public GameObject sparks;
    [ReadOnly] public GameObject Fired;

    void Start()
    {
        damage = (Fired.GetComponent<AttributeManager>()).getAttribute(damgageAttribute);
        pierce = (Fired.GetComponent<AttributeManager>()).getAttribute(pierceAttribute);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null){
            Destroy(this.gameObject);
            return;
        }
        Health collidedHealth = collision.gameObject.GetComponent<Health>();
        Instantiate(sparks, transform.position, transform.rotation, null);
        if (collidedHealth != null)
        {
            if (Fired != null)
                if (Fired.layer != collision.gameObject.layer)
                {
                    collidedHealth.takeDamage(damage.getvalue());
                    collided++;
            }
            if (collision.gameObject.layer == 9)
            {
                Destroy(this.gameObject);
            } else if (pierce.getvalue() > 0)
            {
                for (int i = 0; i < pierce.getvalue(); i++)
                {
                    if (collided > pierce.getvalue())
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
            if(pierce.getvalue() == 0)
                Destroy(this.gameObject);
        }
        else
        {

            Destroy(this.gameObject);
        }
    }
}
