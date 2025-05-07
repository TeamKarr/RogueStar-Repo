using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartManager : MonoBehaviour
{

    public string selectedLevelName;
    // Start is called before the first frame update
    public void StartLevel()
    {
        SceneManager.LoadScene(selectedLevelName);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByName(selectedLevelName));
    }

}
