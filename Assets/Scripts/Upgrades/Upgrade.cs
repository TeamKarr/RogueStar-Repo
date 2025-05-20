using UnityEngine;
using System.Collections;
using System.Linq;
using System;
using UnityEditor;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Simple Upgrade")]
public class Upgrade : ScriptableObject
{

    public string Title;
    public string Description;
    public string[] modifierLabels;
    public ModifierHandler[] AttributeModifiers;

    [SerializeField]
    public UnityEngine.Object[] Components;

    public GameObject[] bullets;
    public bool mergeBullets = false;
    public Upgrade[] requirements;
    public Upgrade[] conflicts;
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


    

    void Enable(GameObject player)
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

    public void Initialise(GameObject player)
    {
        
        // if (bullets.Length != 0)
        // {
        //     var playerBullets = player.GetComponent<ShootingController>().projectilePrefabs;

        //     if (!mergeBullets)
        //     {
        //         playerBullets.Clear();
        //     }
            
        //     foreach (var bullet in bullets)
        //     {
        //         playerBullets.Add(bullet);
        //     }
        // }
       
        
        foreach (UnityEngine.Object c in Components)
        {
            Component cp = player.AddComponent(((MonoScript)c).GetClass());
            //if (cp is UpgradeBehavior ub)
            //{
            //    ub.Start();
            //}

        }
        foreach (var modifier in AttributeModifiers)
        {
            player.GetComponent<AttributeManager>().getAttribute(modifier.attribute).addModifier(modifier.modifier);
        }
    }

    public void Remove(GameObject player)
    {
        foreach (var modifier in AttributeModifiers)
        {
            player.GetComponent<AttributeManager>().getAttribute(modifier.attribute).removeModifier(modifier.modifier);
        }
        foreach (UnityEngine.Object c in Components)
        {
            Destroy(player.GetComponent(c.GetType()));
        }
    }

}
