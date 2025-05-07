using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public Material Shield = null;
    private float baseThickness;
    void Start()
    {
        baseThickness = Shield.GetFloat("_thickness");
        Shield.SetFloat("_base_electric_transprarency", 0.40f);
    }
    public void damage()
    {
        StartCoroutine(visualDamage());
        

    }
    IEnumerator visualDamage()
    {
        Shield.SetFloat("_thickness", 0.50f);
        Shield.SetFloat("_base_electric_transprarency", 0.32f);
        yield return new WaitForSeconds(0.1f);
        Shield.SetFloat("_thickness", 0.45f);
        Shield.SetFloat("_base_electric_transprarency", 0.16f);
        
    }
    // Update is called once per frame
    private float animate =  0f;
    void Update()
    {
        animate = 0.1f*Mathf.Sin(Time.time);
        Debug.Log(animate);
        Shield.SetFloat("_thickness", animate+baseThickness);
        
    }
}
