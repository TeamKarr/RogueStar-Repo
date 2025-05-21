using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{

    public string selectedLevelName;
    // Start is called before the first frame update
    public void StartLevel()
    {
        DontDestroy dontDestroy = FindFirstObjectByType<DontDestroy>();
        if (dontDestroy != null)
        {
            DontDestroy.Instance = null;
            Destroy(dontDestroy.gameObject);
        }


        GameManager gameManager = FindAnyObjectByType<GameManager>();
        gameManager.currentLevel = 0;
        SceneAsset scene = gameManager.currentChapter.levels[gameManager.currentLevel];

        SceneManager.LoadScene(scene.name);
        // var player = GameObject.FindGameObjectWithTag("Player");
    
        // // player.GetComponent<UpgradeManager>().Start();
        // var spawn = GameObject.FindGameObjectWithTag("PlayerSpawn");
        // if (spawn != null)
        // {
        //     player.transform.position = spawn.transform.position;
        // }
        // else
        // {
        //     // Debug.LogError("Spawn point not found");
        // }

        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(selectedLevelName));
    }

}
