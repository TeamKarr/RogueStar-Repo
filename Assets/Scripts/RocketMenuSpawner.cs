using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMenuSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject rocketMenuPrefab;
    public float minDistance = 1.0f;
    public float maxDistance = 3.0f;
    public float minDelay = 1.0f;
    public float maxDelay = 3.0f;
    void Start()
    {
        SpawnRocket();
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    public void SpawnRocket()
    {
        // using the camera's viewport to spawn a rocket at a random position, ensuring at least one coordinate is -0.1 or 1.1.
        if (rocketMenuPrefab == null || Camera.main == null) return;

        GameObject spawnedRocket = Instantiate(rocketMenuPrefab, genPosition(), Quaternion.identity);
        spawnedRocket.GetComponent<RocketMenuMovement>().targetPosition = genPosition();
        spawnedRocket.GetComponent<RocketMenuMovement>().StartMove();
    }

    public Vector3 genPosition()
    {
        float randomX = Random.Range(0, 2) == 0 ? 1.2f : -0.2f;
        float randomY = Random.Range(0, 2) == 0 ? 1.2f : -0.2f;

        if (Random.Range(0, 2) == 0)
        {
            randomX = Random.Range(0.1f, 0.9f);
        }
        else
        {
            randomY = Random.Range(0.1f, 0.9f);
        }


        return Camera.main.ViewportToWorldPoint(new Vector3(randomX, randomY, Random.Range(minDistance, maxDistance)));
    }
}
