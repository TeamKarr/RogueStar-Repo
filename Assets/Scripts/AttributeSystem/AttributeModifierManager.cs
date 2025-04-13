using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeModifierManager : MonoBehaviour
{
    // Start is called before the first frame update

    public AttributeManager target;

    public List<ModifierHandler> modifiers = new ();

    public bool onCollision = true;

    private bool active = false;


    void Start()
    {
        addModifiers ();
    }

    // Update is called once per frame
    void Update()
    {
        //if (onCollision) { 
        //    collision2D.get
        //}
    }

    public void addModifiers()
    {
        foreach(ModifierHandler m in modifiers)
        {
            target.getAttribute(m.attribute).addModifier(m.modifier);
        }
    }

    public void removeModifiers()
    {
        foreach (ModifierHandler m in modifiers)
        {
            target.getAttribute(m.attribute).removeModifier(m.modifier);
        }
    }

    public void enableModifiers()
    {
        foreach (ModifierHandler m in modifiers)
        {
            m.modifier.enabled = true;
        }
    }

    public void disableModifiers()
    {
        foreach (ModifierHandler m in modifiers)
        {
            Debug.Log("Disabled modifiers");
            m.modifier.enabled = false;
        }
    }

    //public void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!active && onCollision)
    //    {
    //        if (target.Equals(collision.gameObject.GetComponent<AttributeManager>()))
    //        {
    //            active = true;
    //            Debug.Log("Object entered trigger: " + collision.gameObject.name);
    //            enableModifiers();
    //        }
    //    }
       
    //}

    //public void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (active) {
    //        if (target.Equals(collision.gameObject.GetComponent<AttributeManager>()))
    //        {
    //            active = false;
    //            Debug.Log("Object exited trigger: " + collision.gameObject.name);
    //            disableModifiers();
    //        }
    //    }
        
    //}

    [System.Serializable]
    public class ModifierHandler
    {
        public string attribute;
        public Attribute.AttributeModifier modifier;
    }
}
