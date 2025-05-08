using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    private Material Shield = null;
    private float baseThickness;
    private float strength = 10;
    void Start()
    {
        Transform t = this.transform;

        Shield = t.GetComponent<ShipParts>().shieldMaterial;
        

        baseThickness = 0.45f;
        Shield.SetFloat("_base_electric_transprarency", 0.40f);
    }
    public void damage()
    {
        if (strength > 0)
        {
            StartCoroutine(visualDamage());
            strength--;
        }

    }
    IEnumerator visualDamage()
    {
        
        baseThickness = 0.50f;
        Shield.SetFloat("_base_electric_transprarency", 0.32f);
        yield return new WaitForSeconds(0.1f);
         baseThickness = 0.45f;
        Shield.SetFloat("_base_electric_transprarency", 0.16f);
        
    }
    // Update is called once per frame
    private float animate =  0f;
    private float animate2 = 0f;

    void Update()
    {
        animate = 0.1f*Mathf.Sin(Time.time);
        
        Shield.SetFloat("_thickness", animate+baseThickness);

        if (strength <= 0)
        {
            StopCoroutine(visualDamage());
            baseThickness +=0.5f;
            Shield.SetFloat("_mix", (10-animate)/10.0f);
            animate2+=0.5f;

        }
        if(animate2>10)
            Destroy(this);

    }
}
