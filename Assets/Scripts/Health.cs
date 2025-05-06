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
    public bool isPlayer = false;
    //public Camera camera;
    public Transform parent;
    public Vector3 offset;

    Attribute maxHealth;

    [ReadOnly] public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
        updateHealthBar();
        maxHealth = (GetComponent<AttributeManager>()).getAttribute(maxHealthAttribute);

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer)
        {
            return;
        }
        if (parent == null)
        {
            parent = this.transform;
        }
        //Debug.Log(Camera.main);
        //healthBar.transform.rotation = Camera.main.transform.rotation;
        //healthBar.transform.position = parent.position + offset;
    }

    public void updateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }

    public void takeDamage(float damage, Collision2D collision)
    {
        // handel if player has shield
        //if (GetComponent<Shield>() != null)
        //{
        //    GetComponent<Shield>().takeDamage(damage, collision);
        //}


        takeDamage(damage);

    }

    public void takeDamage(float damage)
    {
        if (isDead == true)
        {
            //Debug.Log("Already Dead, cannot take damage");
            return;
        }
        health -= damage;
        onDamage.Invoke();
        if (health <= 0)
        {
            die();
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
        isDead = true;
        // do something when the object dies
        
        onDeath.Invoke();

        if (isPlayer)
        {
            gameObject.SetActive(false);
        } else
        {
            Destroy(this.gameObject);
        }
        

    }

}
