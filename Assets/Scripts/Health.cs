using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public float health;
    public string maxHealthAttribute = "MaxHealth";

    public UnityEvent onDeath;
    public UnityEvent onDamage;
    public UnityEvent onHeal;

    public Slider healthBar;

    Attribute maxHealth;

    [ReadOnly] public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetValueWithoutNotify(health);
        updateHealthBar();
        maxHealth = (GetComponent<AttributeManager>()).getAttribute(maxHealthAttribute);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
            Debug.Log("Updated health bar " + health);
        }
    }

    public void takeDamage(float damage)
    {
        if (isDead == true)
        {
            Debug.Log("Already Dead, cannot take damage");
            return;
        }
        health -= damage;
        Debug.Log("Took Damage: " + damage + " Health: " + health + " Max Health: " + maxHealth.getvalue() + " Percentage: " + (health / maxHealth.getvalue()) * 100f + "%");
        onDamage.Invoke();
        if (health <= 0)
        {
            die();
            health = 0;
        }
        updateHealthBar();
    }

    public void heal(float amount)
    {
        health += amount;
        if (health > maxHealth.getvalue())
        {
            health = maxHealth.getvalue();
        }
        updateHealthBar();
    }

    public void setHealth(float amount)
    {
        health = amount;
        if (health > maxHealth.getvalue())
        {
            health = maxHealth.getvalue();
        }
        updateHealthBar();
    }

    public void die()
    {
        // do something when the object dies
        Debug.Log("Died");
        isDead = true;
        onDeath.Invoke();

    }

    
}
