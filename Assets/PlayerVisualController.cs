using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualController : MonoBehaviour
{
    // Start is called before the first frame update
    [ReadOnly] public GameObject gameModelContainer;
    private bool shieldVisual = false;
    private List<MeshRenderer> modelParts = new List<MeshRenderer>();
    private Material shieldMaterial = null;
  
    void Start()
    {
        shieldMaterial = GetComponent<ShipParts>().shieldMaterial;
        
    }
    // Update is called once per frame
    void Update()
    {

        if (GetComponent<ShieldUpgrade>()!=null)
            shieldMaterial.SetFloat("_mix", 1f);
        else
            shieldMaterial.SetFloat("_mix", 0f);
        
    }
}
