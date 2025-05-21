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
        HasRun = true;
        label = GetComponent<TMPro.TextMeshProUGUI>();
        template = label.text;
    }

    public void updateToValue(float value)
    {
        if (!HasRun)
        {
            Start();
        }
        label.text = template + value;
    }
}
