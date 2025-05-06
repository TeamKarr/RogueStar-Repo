using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UIManager : MonoBehaviour
{

    public GameObject upgradePanel;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject gameWindPanel;

    public UpgradeManager upgradeManagerForPlayer;

    private bool isPausable = true;

    // Start is called before the first frame update
    void Start()
    {
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
        CloseAll(false);
        pause();
        isPausable = false;

        // call upgradeManagerForPlayer to set the upgrade choices
        upgradeManagerForPlayer.SetUpgradeChoices();


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
        pausePanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        CloseAll(false);
        pause();
        isPausable = false;
        gameOverPanel.SetActive(true);
    }

    public void ShowGameWinPanel()
    {
        CloseAll(false);
        pause();
        isPausable = false;
        gameWindPanel.SetActive(true);
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
