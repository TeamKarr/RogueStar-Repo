using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;


[System.Serializable]
public class Attribute
{
    /// This is the base class for all attributes
    
    public string name;
    public float baseValue;
    public Details details;

    [System.Serializable]
    public struct Details
    {
        public string description;
        [ReadOnly] public float currentValue;
        public UnityEvent<float> onValueChange;
    }

    [ReadOnly] public List<AttributeModifier> modifiers = new ();

    private void Start()
    {
        updateValue();
    }
    
    [System.Serializable]
    public class AttributeModifier
    {
        public ModifierType type;
        public float value;
        public bool enabled = true;
        public AttributeModifier(ModifierType type, float value)
        {
            this.type = type;
            this.value = value;
        }
    }

    public void addModifier(AttributeModifier m)
    {
        modifiers.Add(m);
        updateValue();
    }

    public void removeModifier(AttributeModifier m)
    {
        modifiers.Remove(m);
        updateValue();
    }


    public void updateValue()
    {
        float baseMultiplier = 1;
        float baseAdder = 0;
        float totalMultiplier = 1;
        foreach (AttributeModifier m in modifiers)
        {
            if (m == null) continue;
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
                        totalMultiplier *= m.value;
                        break;
                }
        }
        details.currentValue = baseValue * baseMultiplier * totalMultiplier + baseAdder;
        details.onValueChange.Invoke(details.currentValue);
    }

    public float getvalue()
    {
        return details.currentValue;
    }

    public void setBaseValue(float value)
    {
        baseValue = value;
        updateValue();
    }

    public enum ModifierType { add, multiply, multiplyBase }
}



[System.Serializable]
public class ModifierHandler
{
    public string attribute;
    public Attribute.AttributeModifier modifier;

    public ModifierHandler(string attribute, Attribute.AttributeModifier modifier)
    {
        this.attribute = attribute;
        this.modifier = modifier;
    }

}