using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{

    [Header("Panels")]
    public GameObject upgradePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameWindPanel;

    [Header("Upgrade Choices")]
    public UpgradeChoice upgradeChoice1;
    public UpgradeChoice upgradeChoice2;
    public UpgradeChoice upgradeChoice3;
    [Header("Upgrade Manager")]
    public UpgradeManager manager;
    public TextMeshProUGUI upgradeHUD;

    

    private bool isPausable = true;

    // Start is called before the first frame update
    void Start()
    {
        upgradeHUDText = upgradeHUD.text;
        var root = GetComponent<UIDocument>().rootVisualElement;
        //root.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);

    }

    public void CloseAll()
    {
        CloseAll(true);
    }

    public void CloseAll(bool shouldUnPause){
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWindPanel.SetActive(false);
        upgradePanel.SetActive(false);

        if (shouldUnPause)
        {
            HideUpgrades();
            WaitForSeconds wait = new WaitForSeconds(0.1f);
            unpause();
        }
        isPausable = true;
    }


    public void pause()
    {
        Time.timeScale = 0f;
        Camera.main.gameObject.GetComponent<CameraPosition>().enabled = false;
    }

    public void unpause()
    {
        Time.timeScale = 1f;
        Camera.main.gameObject.GetComponent<CameraPosition>().enabled = true;
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPausable)
        {
            if (pausePanel.activeSelf)
            {
                CloseAll();
            }
            else
            {
                ShowPausePanel();
            }
        }
    }

    public void ShowPausePanel()
    {
        CloseAll(false);
        pause();
        ShowUpgrades();
        pausePanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        CloseAll(false);
        pause();
        ShowUpgrades();
        isPausable = false;
        gameOverPanel.SetActive(true);
    }

    public void ShowGameWinPanel()
    {
        CloseAll(false);
        pause();
        ShowUpgrades();
        isPausable = false;
        gameWindPanel.SetActive(true);
    }

    private string upgradeHUDText;
    public void ShowUpgrades()
    {
        upgradeHUD.text = upgradeHUDText;
        foreach (var upgrade in manager.activeUpgrades)
        {
            upgradeHUD.text += "\n" + upgrade.name;
        }
        upgradeHUD.gameObject.SetActive(true);
    }

    public void HideUpgrades()
    {
        upgradeHUD.gameObject.SetActive(false);
    }

    //public void OnKeyDown(KeyDownEvent ev)
    //{
    //    Debug.Log(ev.keyCode);
    //    if (ev.keyCode == KeyCode.Escape)
    //    {
    //        if (pausePanel.activeSelf)
    //        {
    //            CloseAll();
    //        }
    //        else
    //        {
    //            ShowPausePanel();
    //        }
    //    }
    //}


}
