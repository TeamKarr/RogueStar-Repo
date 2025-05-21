using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AnimatedValues;
using UnityEngine;
using static GameManager;
using static UnityEditor.VersionControl.Asset;

public class AttributeManager : MonoBehaviour
{
   
    //public List<(string, bool)> booleans = new List<(string, bool)>();

    private Dictionary<string,Attribute> attributes = new Dictionary<string, Attribute>();

    public List<Attribute> Attributes = new List<Attribute>();

    public Attribute getAttribute(string name)
    {
        if (attributes.ContainsKey(name) == false)
        {
            Debug.LogError($"Attribute {name} not found");
            return null;
        }
        // return Attributes.FirstOrDefault(x => x.name == name);
        return attributes[name];

    }

    void Awake()
    {
        InitializeAttributes();
    }
    private void InitializeAttributes()
    {
        attributes = new Dictionary<string, Attribute>();
        foreach (var a in Attributes)
        {
            attributes[a.name] = a;
            a.updateValue();
        }
    }

}
