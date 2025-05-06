using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    public int Matter;
    public int Score;

    public string StationScene;

    private GameManager gameManager;

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
            gameManager.addMatter(Matter);
            gameManager.addScore(Score);
        }
        

        SceneManager.LoadScene(StationScene);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(StationScene));
    }








}
