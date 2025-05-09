using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipParts : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject ship;
    public Material shieldMaterial;
    [ReadOnly] public List<GameObject> boosterObjects = new List<GameObject>();
    public List<ParticleSystem> boosterParticles = new List<ParticleSystem>();
    void Start()
    {
        ran = true;
        Debug.Log("ship parts");
        Transform t = this.transform;
        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == "booster")
            {
                boosterObjects.Add(t.GetChild(i).gameObject);
            }
            if (t.GetChild(i).gameObject.tag == "boosterParticle")
            {
                boosterParticles.Add(t.GetChild(i).GetComponent<ParticleSystem>());
            }

        }

    }
    bool ran = false;
    public void initialize (){
        if (!ran){
            Start();
            
        }
    }


    // Update is called once per frame

}
