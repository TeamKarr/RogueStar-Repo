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

    private GameManager gameManager;
    

    public List<GameObject> Enemies;
    public List<GameObject> Bosses;

    public LevelWinType[] howToWin;

    public bool isLastLevel = false;

    public enum LevelWinType
    {
        KillAllEnemies,
        KillBoss,
    }




    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
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

    }

    public void returnToStation(bool shouldSave)
    {
        if (shouldSave)
        {
            if (gameManager != null)
            {
                gameManager.addMatter(Matter);
                gameManager.addScore(Score);
            }
            
        }
        

        SceneManager.LoadScene(StationScene);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(StationScene));
    }

    // How do we handle winning?

    public void checkIfWon()
    {
        // Check if has won
        if (howToWin.All(a => a switch { LevelWinType.KillAllEnemies => Enemies.Count == 0, LevelWinType.KillBoss => Bosses.Count == 0, _ => false }))
        {
            if (isLastLevel)
            {
                ui.ShowGameWinPanel();
            }
            else
            {
                GameManager.NextLevel();
            }

        }
    }
}
