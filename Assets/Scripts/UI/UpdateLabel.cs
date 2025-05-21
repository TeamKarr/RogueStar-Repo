using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateLabel : MonoBehaviour
{
    // Start is called before the first frame update

    private TMPro.TextMeshProUGUI label;
    private string template;

    private bool HasRun = false;
    void Start()
    {
        if (HasRun)
        {
            return;
        }
        HasRun = true;
        label = GetComponent<TMPro.TextMeshProUGUI>();
        template = label.text;
        //Debug.Log("template: " + template);
    }

    public void updateToValue(float value)
    {
        //Debug.Log("Updated to " + template + "|" + value);
        if (!HasRun)
        {
            Start();
        }
        label.text = template + value;
    }
}
