using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public int xp = 1;
    public int enemyLayer = 7;
    private GameObject[] allObjects;


    // Start is called before the first frame update
    void Start()
    {
        allObjects = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> objectsOnLayer = new List<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == enemyLayer)
            {
                objectsOnLayer.Add(obj);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in allObjects)
        {
            Health health = obj.GetComponent<Health>();
            if (health != null)
            {
                if (health.isDead == true)
                {
                    Instantiate(this.gameObject, health.deathpos, Quaternion.identity, transform);
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
