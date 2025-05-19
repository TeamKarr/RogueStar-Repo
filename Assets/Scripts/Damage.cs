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

    [HideInInspector] public GameObject Fired;

    void Start()
    {
        damage = Fired.GetComponent<AttributeManager>().getAttribute(damgageAttribute);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null){
            Destroy(this.gameObject);
            return;
        }
        Health collidedHealth = collision.gameObject.GetComponent<Health>();
        if (collidedHealth != null)
        {
            if (Fired != null)
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
