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

    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        //root.RegisterCallback<KeyDownEvent>(OnKeyDown, TrickleDown.TrickleDown);

    }

    public void CloseAll(bool shouldUnPause = true){
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        gameWindPanel.SetActive(false);
        upgradePanel.SetActive(false);
        if (shouldUnPause)
        {
            Time.timeScale = 1f;
        }
    }

    public void ShowUpgradePanel()
    {
        CloseAll(false);
        Time.timeScale = 0f;
        // call upgradeManagerForPlayer to set the upgrade choices
        upgradeManagerForPlayer.SetUpgradeChoices();


        upgradePanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
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
