using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScript : MonoBehaviour
{

    public enum MenuStates { Main, Vehicle, Dive};
    public MenuStates currentMenuState;

    public Canvas mainMenu;
    public Canvas vehicleScreen;
    public Canvas diveScreen;
    
    public GameObject diveSession;
    public GameObject arSession;
    public Material blackSkybox;
    public Material oceanSkybox;

    private Material defaultSkybox;
    public ARTapToPlaceObject arTapScript;
    public DiveStartup diveStartupScript;
    public ARStartup arStartupScript;

    void Start()
    {
        currentMenuState = MenuStates.Main;
        defaultSkybox = RenderSettings.skybox;
    }

    void Update()
    {
        switch (currentMenuState)
        {
            case MenuStates.Main:
                //display correct buttons
                mainMenu.gameObject.SetActive(true);
                vehicleScreen.gameObject.SetActive(false);
                diveScreen.gameObject.SetActive(false);

                //get correct scene setup
                diveSession.SetActive(false);
                arSession.SetActive(false);
                RenderSettings.skybox = defaultSkybox;

                break;

            case MenuStates.Vehicle:
                //display correct buttons
                mainMenu.gameObject.SetActive(false);
                vehicleScreen.gameObject.SetActive(true);
                diveScreen.gameObject.SetActive(false);
               
                //get correct scene setup
                diveSession.SetActive(false);
                arSession.SetActive(true);
                RenderSettings.skybox = defaultSkybox;

                break;

            case MenuStates.Dive:
                //display correct buttons
                mainMenu.gameObject.SetActive(false);
                vehicleScreen.gameObject.SetActive(false);
                diveScreen.gameObject.SetActive(true);


                //get correct scene setup
                arSession.SetActive(false);
                diveSession.SetActive(true);
                //RenderSettings.skybox = oceanSkybox;
                //Camera.current.GetComponent<Skybox>().enabled = false;

                break;
        }
    }

    public MenuStates getMenuState()
    {
        return currentMenuState;
    }

    //Main Menu
    public void VehicleButtonPressed()
    {
        currentMenuState = MenuStates.Vehicle;
        arStartupScript.startAR();
    }

    public void DiveButtonPressed()
    {
        currentMenuState = MenuStates.Dive;
        diveStartupScript.startDiveSession();
        diveStartupScript.resetHerc();
    }

    //AR Vehicle Buttons
    public void HercGraphicPressed()
    {
        arTapScript.TransitionToHercARState();
        arTapScript.DestroyOrPlaceObject();

    }

    public void ArgusGraphicPressed()
    {
        arTapScript.TransitionToArgusARState();
        arTapScript.DestroyOrPlaceObject();
    }

    //back button for both AR session and Dive session
    public void BackToMain()
    {
        currentMenuState = MenuStates.Main;
        arTapScript.DestroyAllObjects();
        diveStartupScript.endDiveSession();
    }
}
