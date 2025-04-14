using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public float health;
    public string maxHealthAttribute = "MaxHealth";

    public UnityEvent onDeath;
    public UnityEvent onDamage;

    Attribute maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = (GetComponent<AttributeManager>()).getAttribute(maxHealthAttribute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        Debug.Log("Took Damage: " + damage + " Health: " + health + " Max Health: " + maxHealth.getvalue() + " Percentage: " + (health / maxHealth.getvalue()) * 100f + "%");
        onDamage.Invoke();
        if (health <= 0)
        {
            die();
        }
    }

    public void heal(float amount)
    {
        health += amount;
        if (health > maxHealth.getvalue())
        {
            health = maxHealth.getvalue();
        }
    }

    public void setHealth(float amount)
    {
        health = amount;
        if (health > maxHealth.getvalue())
        {
            health = maxHealth.getvalue();
        }
    }

    public void die()
    {
        // do something when the object dies
        Debug.Log("Died");

        onDeath.Invoke();
    }

    public void log(string message)
    {
        Debug.Log(message);
    }
}
