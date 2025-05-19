using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaiveManager : MonoBehaviour
{
    // Start is called before the first frame update

    public List<GameObject> enemies;

    public float startDelay = 0.5f;
    public bool startWhenPreviousWaiveEnds = false;
    public float delay = 0.5f;
    public SpawnType spawnType = SpawnType.Order;
    public bool IsComplete {get { return enemies.All(e => e == null || e.activeSelf == false);}}

    public enum SpawnType
    {
        Order,
        Random,
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void StartWaive()
    {
        StartCoroutine(Waive());
    }

    IEnumerator Waive()
    {
        enemies = GetComponentsInChildren<Transform>(true).Select(t => t.gameObject).ToList();
        enemies.RemoveAt(0);
        Debug.Log("Enemies: " + enemies.Count);
        if (spawnType == SpawnType.Random)
        {
            enemies = enemies.OrderBy(e => Random.value).ToList();
        }
        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;
            enemy.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }

}
