using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject nav;
    public GameObject navSene;
    public GameObject mech;
    public GameObject mechSene;
    public GameObject launch;
    public GameObject launchSene;


    public Button navButton;
    public Button mechButton;
    public Button launchButton;


    public UpdateLabel scoreLabel;
    public UpdateLabel matterLabel;

    public MechanicUpgrade[] mechanicUpgrades;

    // Start is called before the first frame update
    void Start()
    {
        goToNav();
        updateMenus();
    }

    public void goToNav()
    {
        nav.SetActive(true);
        navSene.SetActive(true);
        mech.SetActive(false);
        mechSene.SetActive(false);
        launch.SetActive(false);
        launchSene.SetActive(false);
        navButton.interactable = false;
        mechButton.interactable = true;
        launchButton.interactable = true;
    }
    public void goToMech()
    {
        nav.SetActive(false);
        navSene.SetActive(false);
        mech.SetActive(true);
        mechSene.SetActive(true);
        launch.SetActive(false);
        launchSene.SetActive(false);
        navButton.interactable = true;
        mechButton.interactable = false;
        launchButton.interactable = true;
    }
    public void goToLaunch() {
        nav.SetActive(false);
        navSene.SetActive(false);
        mech.SetActive(false);
        mechSene.SetActive(false);
        launch.SetActive(true);
        launchSene.SetActive(true);
        navButton.interactable = true;
        mechButton.interactable = true;
        launchButton.interactable = false;
    }

    public void updateMenus()
    {
        Debug.Log("Updating menus");
        GameManager manager = FindAnyObjectByType<GameManager>();
        scoreLabel.updateToValue(manager.score);
        matterLabel.updateToValue(manager.matter);

        foreach (var m in mechanicUpgrades)
        {
            m.updateDisplay();
        }
    }
}
