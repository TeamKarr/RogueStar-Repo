using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.RuleTile.TilingRuleOutput;


public class AttributeT : MonoBehaviour
{
    /// This is the base class for all attributes
    
    public new string name;
    public string description;
    public float baseValue;

    float baseMultiplier = 1;
    float baseAdder = 0;
    float totalMultiplier = 1;

    [ReadOnly] public float currentValue = 0;

    public UnityEvent<float> onValueChange;

    //[ReadOnly] public List<AttributeModifier> modif= new ();
    [ReadOnly] public List<AttributeModifier> modifiers = new ();

    private void Start()
    {
        updateValue();
    }

    private void FixedUpdate()
    {
        //if (Input.GetKeyDown("q") == true)
        //{
        //    totalMultiplier = incM(totalMultiplier);
        //    updateValue();
        //}
        //if (Input.GetKeyDown("z") == true)
        //{
        //    totalMultiplier = decM(totalMultiplier);
        //    updateValue();
        //}
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
        currentValue = baseValue * baseMultiplier * totalMultiplier + baseAdder;
        //Debug.Log("Updated Attribute: " + name + " Base Value: " + baseValue + " Base Multiplier: " + baseMultiplier + " Base Adder: " + baseAdder + " Total Multiplier: " + totalMultiplier + " Current Value: " + currentValue + " Modifiers: " + modifiers.Count);
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
    //public float incM(float multiplier)
    //{
    //    return multiplier += 1;
    //}
    //public float decM(float multiplier)
    //{
    //    return multiplier -=1;
    //}
}



//[System.Serializable]
//public class ModifierHandler
//{
//    public string attribute;
//    public Attribute.AttributeModifier modifier;

//    public ModifierHandler(string attribute, Attribute.AttributeModifier modifier)
//    {
//        this.attribute = attribute;
//        this.modifier = modifier;
//    }

//}