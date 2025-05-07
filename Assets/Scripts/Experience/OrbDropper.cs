using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbDropper : MonoBehaviour
{
    private Health health;
    public GameObject orbPrefab;
    public int dropNum = 5;
    private Vector3 offset;
    void Start()
    {
        health = gameObject.GetComponent<Health>();
    }


    void Update()
    {
    }
    

    public void SpawnOrb()
    {
        if (orbPrefab != null)
        {
            Vector3 pos = health.deathpos;
            // Create the projectile
            for (int i = 0; i < dropNum; i++)
            {
                offset = new Vector3(Random.Range(-.1f, .1f), Random.Range(-.1f, .1f), Random.Range(-.1f, .1f));
                Instantiate(orbPrefab, pos+offset, transform.rotation, null);
            }

        }
    }
}
