using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private Camera _camera;
    public Transform parent;
    public Vector3 offset;

    Attribute maxHealth;

    [ReadOnly] public bool isDead = false;
    [HideInInspector] public Vector3 deathpos;

    // Start is called before the first frame update
    void Start()
    {
        if (parent == null)
        {
            parent = this.transform;
        }
        _camera = Camera.main;
        if (this.gameObject.layer == 7)
        {

            var healthBarCanvasInstance = Instantiate(healthBarCanvas, transform.position, Quaternion.identity, transform);

            healthBar = healthBarCanvasInstance.GetComponentInChildren<Slider>();
        }
        else if (this.gameObject.layer == 3)
        {
            Transform childTransform = GameObject.FindWithTag("healthBar").transform;
            healthBar = childTransform.GetComponent<Slider>();
        }
        savedMaxHealth = health;
        maxHealth = GetComponent<AttributeManager>().getAttribute(maxHealthAttribute);
        if (maxHealth == null)
        {
            Debug.LogError(gameObject.name + " Max Health attribute not found");
            // Debug.LogError("Max Health attribute not found");

        }
        else
        {
            Debug.Log("Max Health attribute found: " + maxHealth.getvalue());
        }


        // healthBar.maxValue = maxHealth.getvalue();

        UpdateMaxHealth();
        updateHealthBar();
    }

    public void UpdateMaxHealth()
    {

        var healthRatio = health / savedMaxHealth;

        if (maxHealth != null)
            return;
        savedMaxHealth = maxHealth.getvalue();

        health = savedMaxHealth * healthRatio;

        if (healthBar != null)
            healthBar.maxValue = savedMaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.layer == 7)
        {
            if (healthBar != null)
            {
                healthBar.transform.SetPositionAndRotation(parent.position + offset, _camera.transform.rotation);
            }
            
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
        if (GetComponent<ShieldUpgrade>() != null)
        {
            GetComponent<ShieldUpgrade>().damage(damage);
        }
        else
        {
            takeDamage(damage);
        }
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

        FindAnyObjectByType<LevelManager>().Matter += (int)Mathf.Max(Mathf.Ceil(health / 5f),1);
        FindAnyObjectByType<LevelManager>().Score += (int)Mathf.Max(Mathf.Ceil(health / 2f),1);


        if (this.gameObject.layer == 3)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }
        

    }

}
