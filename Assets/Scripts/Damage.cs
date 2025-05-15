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
    public GameObject sparks;
    [ReadOnly] public GameObject Fired;

    void Start()
    {
        damage = (GetComponent<AttributeManager>()).getAttribute(damgageAttribute);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health collidedHealth = collision.gameObject.GetComponent<Health>();
        Instantiate(sparks, transform.position, transform.rotation, null);
        if (collidedHealth != null)
        {
            if (Fired.layer != collision.gameObject.layer)
            {
                collidedHealth.takeDamage(damage.getvalue());
            }
            
            Destroy(this.gameObject);
        }
        else
        {

            Destroy(this.gameObject);
        }
    }
}
