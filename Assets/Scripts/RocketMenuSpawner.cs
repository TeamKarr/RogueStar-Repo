using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMenuSpawner : MonoBehaviour
{
    public GameObject rocketMenuPrefab;
    public float minDistance = 1.0f;
    public float maxDistance = 3.0f;
    public float minDelay = 1.0f;
    public float maxDelay = 3.0f;
    public int maxRockets = 5;

    private int currentRocketCount = 0;

    void Start()
    {
        StartCoroutine(SpawnRocketsOverTime());
    }

    public void SpawnRocket()
    {
        if (rocketMenuPrefab == null || Camera.main == null || currentRocketCount >= maxRockets) return;

        GameObject spawnedRocket = Instantiate(rocketMenuPrefab, genPosition(), Quaternion.identity);
        spawnedRocket.GetComponent<RocketMenuMovement>().targetPosition = genPosition();
        spawnedRocket.GetComponent<RocketMenuMovement>().StartMove();

        currentRocketCount++;
        spawnedRocket.GetComponent<RocketMenuMovement>().OnRocketDestroyed += () => currentRocketCount--;
        // randomize acceleration and speed and max speed:
        RocketMenuMovement rocketMovement = spawnedRocket.GetComponent<RocketMenuMovement>();
        rocketMovement.acceleration = Random.Range(0.1f, 3.0f);
        rocketMovement.speed = Random.Range(1f, 5.0f);
        rocketMovement.maxSpeed = Random.Range(2.0f, 10.0f);
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

    private IEnumerator SpawnRocketsOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            SpawnRocket();
        }
    }
}
