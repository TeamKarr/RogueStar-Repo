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

    private bool hasChecked = false;

    public Attribute getAttribute(string name)
    {
        if (!hasChecked)
        {
            Start();
        }
        return attributes[name];
    }

    void Start()
    {
        if (hasChecked) return;
        hasChecked = true;
        var attributeArray = GetComponents<Attribute>();
        foreach (Attribute a in attributeArray)
        {
            attributes.Add(a.name, a);
            Debug.Log("Attributes: " + a.name);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
