using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    public int Matter = 0;
    public int Score = 0;

    public string StationScene;

    public UIManager ui;

    public List<WaiveManager> waives = new();
    [ReadOnly] public List<WaiveManager> activeWaives =new();

    private GameManager gameManager;
    

    public List<GameObject> Enemies
    {
        get
        {
            return GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }
    }
    public List<GameObject> Bosses
    {
        get
        {
            return GameObject.FindGameObjectsWithTag("Boss").ToList();
        }
    }

    public LevelWinType[] howToWin;

    // public bool isLastLevel = false;

    public enum LevelWinType
    {
        KillAllEnemies,
        KillBoss,
        NoWaivesLeft,
    }




    void Start()
    {
        Debug.Log("LevelManager started");
        gameManager = FindFirstObjectByType<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        if (gameManager != null)
        {
            var playerAttributes = player.GetComponent<AttributeManager>();
            if (playerAttributes != null)
            {
                foreach (var item in gameManager.defaultAttributes.Keys)
                {
                    playerAttributes.getAttribute(item).setBaseValue(gameManager.defaultAttributes[item].amount);
                }
            }
        }

        FindFirstObjectByType<UIManager>().CloseAll();

        if (waives.Count > 0)
        {
            StartCoroutine(runWaive());
        }
        else
        {
            Debug.Log("No waives to run");
        }

    }

    public void returnToStation(bool shouldSave)
    {
        //DontDestroy.Instance = null;
        Destroy(player);
        Debug.Log("returning to station");
        
        if (shouldSave)
        {
            if (gameManager != null)
            {
                gameManager.addMatter(Matter);
                gameManager.addScore(Score);
            }

        }
        SceneManager.LoadScene(StationScene);

        //gameManager.updateMenuHead();




        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(StationScene));
    }

    // How do we handle winning?

    void Update()
    {
        checkWaives();
        checkIfWon();
    }

    public void checkWaives()
    {
        foreach (var waive in activeWaives)
        {
            if (waive.IsComplete)
            {
                activeWaives.Remove(waive);
                waive.gameObject.SetActive(false);
                break;
            }
        }
    }


    IEnumerator runWaive()
    {
        if (waives.Count > 0)
        {
            var currentWaive = waives.First();
            if (currentWaive != null)
            {
                if (currentWaive.startWhenPreviousWaiveEnds)
                {
                    Debug.Log("Waiting for previous waive to end");
                    var activeWaive = activeWaives.LastOrDefault();
                    yield return new WaitUntil(() => activeWaive == null || activeWaive.IsComplete);
                }
                else
                {
                    yield return new WaitForSeconds(currentWaive.startDelay);
                }
                currentWaive.gameObject.SetActive(true);
                currentWaive.StartWaive();
                activeWaives.Add(currentWaive);
                waives.Remove(currentWaive);
                StartCoroutine(runWaive());
            }
        }
    }
    

    public void checkIfWon()
    {
        // Debug.Log("Checking if won");
        // Check if has won
        if (howToWin.All(a => a switch { LevelWinType.KillAllEnemies => Enemies.Count == 0, LevelWinType.KillBoss => Bosses.Count == 0, LevelWinType.NoWaivesLeft => waives.Count == 0 && activeWaives.Count == 0, _ => false }))
        {
            Debug.Log("You won!");
            // go to next level
            if (!gameManager.NextLevel())
            {
                ui.ShowGameWinPanel();
            }

        }
    }
}
