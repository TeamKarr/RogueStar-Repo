using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject upgradePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameWindPanel;

    [Header("Upgrade Choices")]
    public UpgradeChoice upgradeChoice1;
    public UpgradeChoice upgradeChoice2;
    public UpgradeChoice upgradeChoice3;
    [Header("Upgrade Manager")]
    public MechanicUpgrade manager;

    public void CloseAll(){
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWindPanel.SetActive(false);
        upgradePanel.SetActive(false);
    }

    public void ShowUpgradePanel()
    {
        CloseAll();
        Time.timeScale = 0;
        // call upgradeManagerForPlayer to set the upgrade choices

        (Upgrade, Upgrade, Upgrade) choices = manager.GetUpgradeChoices();
        
        upgradeChoice1.SetUpgrade(choices.Item1);
        upgradeChoice2.SetUpgrade(choices.Item2);
        upgradeChoice3.SetUpgrade(choices.Item3);

        upgradePanel.SetActive(true);
    }
}
