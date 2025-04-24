using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTitleAttribute : Attribute
{

    public string title { get; private set; }
    public string[] argNames { get; private set; }

    public DynamicTitleAttribute(string title, params string[] argNames)
    {
        this.title = title;
        this.argNames = argNames;
    }


}
