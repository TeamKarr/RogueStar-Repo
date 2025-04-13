using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Attribute : MonoBehaviour
{

    public string name;
    public string description;
    public float baseValue;

    //[ReadOnly]
    public float currentValue = 0;

    public HashSet<AttributeModifier> modifiers = new ();

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }
    
    public class AttributeModifier
    {
        public ModifierType type;
        public float value;
        public bool enabled = true;
        public string tag;


    }

    public void addModifier(AttributeModifier m)
    {
        modifiers.Add(m);
    }

    public void removeModifier(AttributeModifier m)
    {
        modifiers.Remove(m);
    }

    public float getvalue()
    {
        float baseMultiplier = 1;
        float baseAdder = 0;
        float totalMultiplier = 1;
        foreach (AttributeModifier m in modifiers)
        {
            if (m.enabled)
                switch (m.type)
                {
                    case ModifierType.add:
                        baseAdder += m.value;
                        break;
                    case ModifierType.multiplyBase:
                        baseMultiplier += m.value;
                        break;
                    case ModifierType.multiply:
                        totalMultiplier += m.value;
                        break;
                }
        }
        currentValue = baseValue * (baseMultiplier + baseAdder) * totalMultiplier;
        return baseValue * (baseMultiplier + baseAdder) * totalMultiplier;
    }

    public enum ModifierType { add, multiply, multiplyBase }
}
