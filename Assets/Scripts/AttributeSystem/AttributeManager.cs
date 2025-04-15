using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class AttributeManager : MonoBehaviour
{
    // Start is called before the first frame update

    private Dictionary<string,Attribute> attributes = new Dictionary<string, Attribute>();

    public Attribute getAttribute(string name)
    {
        return attributes[name];
    }

    void Start()
    {
        var attributeArray = GetComponents<Attribute>();
        foreach (Attribute a in attributeArray)
        {
            attributes[a.name] = a;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
