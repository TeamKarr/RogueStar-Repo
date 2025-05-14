using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    

    [HideInInspector] public GameObject Fired;

    void Start()
    {
        damage = (Fired.GetComponent<AttributeManager>()).getAttribute(damgageAttribute);
        pierce = (Fired.GetComponent<AttributeManager>()).getAttribute(pierceAttribute);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health collidedHealth = collision.gameObject.GetComponent<Health>();
        if (collidedHealth != null)
        {
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
