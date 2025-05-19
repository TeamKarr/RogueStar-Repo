using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health;
    private float savedMaxHealth;
    public string maxHealthAttribute = "MaxHealth";
    

    public UnityEvent onDeath;
    public UnityEvent onDamage;
    public UnityEvent onHeal;


    public Canvas healthBarCanvas;

    [Header("For Enemy HealthBars")]
    private Slider healthBar;
    public new Camera camera;
    public Transform parent;
    public Vector3 offset;

    Attribute maxHealth;

    [ReadOnly] public bool isDead = false;
    [HideInInspector] public Vector3 deathpos;

    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.layer == 7)
        {
            
            var healthBarCanvasInstance = Instantiate(healthBarCanvas, transform.position, Quaternion.identity, transform);
            
            healthBar = healthBarCanvasInstance.GetComponentInChildren<Slider>();
        } else if (this.gameObject.layer == 3)
        {
            Transform childTransform = healthBarCanvas.transform.Find("PlayerHealthBar");
            healthBar = childTransform.GetComponent<Slider>();
        }
        savedMaxHealth = health;
        maxHealth = (GetComponent<AttributeManager>()).getAttribute(maxHealthAttribute);
        UpdateMaxHealth();
        
        healthBar.maxValue = maxHealth.getvalue();
        updateHealthBar();
    }

    public void UpdateMaxHealth()
    {

        var healthRatio = health / savedMaxHealth;

        savedMaxHealth = maxHealth.getvalue();

        health = savedMaxHealth * healthRatio;

        healthBar.maxValue = savedMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.layer == 7)
        {
            healthBar.transform.SetPositionAndRotation(parent.position + offset, camera.transform.rotation);
        }
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
        deathpos = this.transform.position;
        onDeath.Invoke();
        // do something when the object dies
        

        if (this.gameObject.layer == 3)
        {
            gameObject.SetActive(false);
        } else
        {
            Destroy(this.gameObject);
        }
        

    }

}
