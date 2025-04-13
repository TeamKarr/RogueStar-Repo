using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AnimatedValues;
using UnityEngine;

public class AttributeManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Attribute> attributes = new();

    public Attribute getAttribute(string name)
    {
        foreach (Attribute a in attributes)
        {
            if (a.name == name)
            {
                return a;
            }
        }
        return null;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
