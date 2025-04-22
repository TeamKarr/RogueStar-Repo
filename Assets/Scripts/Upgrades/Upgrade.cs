using UnityEngine;
using System.Collections;
using System.Linq;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Simple Upgrade")]
public class Upgrade : ScriptableObject
{

    public string Title;
    public string Description;
    public string[] modifierLabels;
    public ModifierHandler[] AttributeModifiers;

    public float weight = 1;
 
    public string[] ModifierLabels
    {
        get
        {
            var output = new string[AttributeModifiers.Length];
            for (int i = 0; i < AttributeModifiers.Length; i++)
            {
                string prefix = "";
                string type = "+";
                switch (AttributeModifiers[i].modifier.type)
                {
                    case Attribute.ModifierType.add:
                        prefix = "+ ";
                        if (AttributeModifiers[i].modifier.value < 0)
                        {
                            type = "-";
                        }
                        break;
                    case Attribute.ModifierType.multiply:
                        prefix = "x";
                        if (AttributeModifiers[i].modifier.value < 1)
                        {
                            type = "-";
                        }
                        break;
                    case Attribute.ModifierType.multiplyBase:
                        prefix = "+ x";
                        if (AttributeModifiers[i].modifier.value < 0)
                        {
                            type = "-";
                        }
                        break;
                }

                output[i] = $"{type+prefix}{AttributeModifiers[i].modifier.value} {AttributeModifiers[i].attribute}";
            }
            return modifierLabels.Concat(output).ToArray();
        }
    }


    

    void Enable()
    {
        foreach (ModifierHandler m in AttributeModifiers)
        {
            m.modifier.enabled = true;
        }
    }

    void Disable()
    {
        foreach (ModifierHandler m in AttributeModifiers)
        {
            m.modifier.enabled = false;
        }
    }

}
