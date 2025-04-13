using UnityEngine;

public class ConditionalPropertyAttribute : PropertyAttribute
{

    public string condition;

    public ConditionalPropertyAttribute(string condition)
    {
        this.condition = condition;
    }
}