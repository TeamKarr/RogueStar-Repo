using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.AnimatedValues;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class AttributeManager : MonoBehaviour
{
   
    //public List<(string, bool)> booleans = new List<(string, bool)>();

    private Dictionary<string,Attribute> attributes = new Dictionary<string, Attribute>();

    private bool hasChecked = false;

    //public bool boolean(string name)
    //{
    //    foreach (var b in booleans)
    //    {
    //        if (b.Item1 == name)
    //        {
    //            return b.Item2;
    //        }
    //    }
    //    return false;
    //}

    //public bool toggle(string name)
    //{
    //    for (int i = 0; i < booleans.Count; i++)
    //    {
    //        if (booleans[i].Item1 == name)
    //        {
    //            booleans[i].Item2 = !b.Item2;
    //            return b.Item2;
    //        }
    //    }
    //}

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
            //Debug.Log("Attributes: " + a.name);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

}
