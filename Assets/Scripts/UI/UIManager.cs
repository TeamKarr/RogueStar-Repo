using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject upgradePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameWindPanel;

    public UpgradeManager upgradeManagerForPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CloseAll(){
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWindPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }

    public void ShowUpgradePanel()
    {
        CloseAll();

        // call upgradeManagerForPlayer to set the upgrade choices
        upgradeManagerForPlayer.SetUpgradeChoices();


        upgradePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
