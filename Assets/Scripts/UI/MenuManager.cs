using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public GameObject nav;
    public GameObject mech;
    public GameObject launch;

    public Button navButton;
    public Button mechButton;
    public Button launchButton;

    // Start is called before the first frame update
    void Start()
    {
        goToNav();
    }

    public void goToNav()
    {
        nav.SetActive(true);
        mech.SetActive(false);
        launch.SetActive(false);
        navButton.interactable = false;
        mechButton.interactable = true;
        launchButton.interactable = true;
    }
    public void goToMech()
    {
        nav.SetActive(false);
        mech.SetActive(true);
        launch.SetActive(false);
        navButton.interactable = true;
        mechButton.interactable = false;
        launchButton.interactable = true;
    }
    public void goToLaunch() {
        nav.SetActive(false);
        mech.SetActive(false);
        launch.SetActive(true);
        navButton.interactable = true;
        mechButton.interactable = true;
        launchButton.interactable = false;
    }
}
