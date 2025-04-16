using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Attribute : MonoBehaviour
{

    public new string name;
    public string description;
    public float baseValue;

    [ReadOnly] public float currentValue = 0;

    public UnityEvent<float> onValueChange;

    public HashSet<AttributeModifier> modifiers = new ();

    private void Start()
    {
        updateValue();
    }

    private void Update()
    {
        
    }
    
    public class AttributeModifier
    {
        public ModifierType type;
        public float value;
        public bool enabled = true;
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
        Debug.Log("Updated Attribute: " + name + " Base Value: " + baseValue + " Base Multiplier: " + baseMultiplier + " Base Adder: " + baseAdder + " Total Multiplier: " + totalMultiplier + " Current Value: " + currentValue + " Modifiers: " + modifiers.Count);
        onValueChange.Invoke(currentValue);
    }

    public float getvalue()
    {
        return currentValue;
    }

    public void setBaseValue(float value)
    {
        baseValue = value;
        updateValue();
    }

    public enum ModifierType { add, multiply, multiplyBase }
}
