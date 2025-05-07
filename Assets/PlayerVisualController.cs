using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    // Start is called before the first frame update
    [ReadOnly] public GameObject gameModelContainer;
    public bool shieldVisual = false;
    private List<MeshRenderer> modelParts = new List<MeshRenderer>();
    public Material shieldMaterial = null;
    public Material noShieldMaterial = null;
    void Start()
    {
        Transform t = this.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == "Player Model")
            {
                gameModelContainer = t.GetChild(i).gameObject;
                Debug.Log("success");
                break;
            }

        }
        
    }
    // Update is called once per frame
    void Update()
    {
        if (shieldVisual == true)
            shieldMaterial.SetFloat("_mix", 1f);
        else
            shieldMaterial.SetFloat("_mix", 0f);
        
    }
}
