using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]

public class ARTapToPlaceObject : MonoBehaviour
{
    /// <summary>
    /// This controls the placement of Augmented Reality objects and the placement logo. 
    /// </summary>
    ///

    [SerializeField]

    public GameObject placementIndicator;
    public GameObject hercGameObj;
    public GameObject argusGameObj;
    public GameObject environment;
    public GameObject blackcube;
    public MenuScript menuScript;
    public GameObject slider;

    private ARRaycastManager arRaycastManager;
    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool hercIsPlaced = false;
    private bool argusIsPlaced = false;
    private bool environmentIsPlaced = false;
    private GameObject instantiatedHerc;
    private GameObject instantiatedArgus;
    private GameObject instantiatedEnv;
    private GameObject instatiatedBlackCube;


    public enum ARStates { Herc, Argus, Dive };
    public ARStates currentARState;

    // Start is called before the first frame update
    void Start()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        currentARState = ARStates.Herc;
    }

    // Update is called once per frame
    void Update()
    {
        


        switch (menuScript.currentMenuState)
        {
            case (MenuScript.MenuStates.Main):
                break;

            case (MenuScript.MenuStates.Vehicle):
                UpdatePlacementPose();
                UpdatePlacementIndicator();
                break;

            case (MenuScript.MenuStates.Dive):
                break;

        }
    }

    private void UpdatePlacementIndicator()
    {
        //controls the position of the placement logo on the plane
        if (placementPoseIsValid && !hercIsPlaced && !argusIsPlaced)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }

    }

    private void UpdatePlacementPose()
    {
        try
        {
            var screenCenter = Camera.current.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            arRaycastManager.Raycast(screenCenter, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);
            placementPoseIsValid = hits.Count > 0;

            if (placementPoseIsValid)
            {
                placementPose = hits[0].pose;
                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
        catch (System.NullReferenceException)
        {
            placementPose = new Pose();
        }
    }

    public void DestroyOrPlaceObject()
    {
        switch (currentARState) {

            case ARStates.Herc:
                if (hercIsPlaced)
                {
                    DestroyHerc();
                }
                else
                {
                    placeHerc();
                }
                break;

            case ARStates.Argus: 
                if (argusIsPlaced)
                {
                    DestroyArgus();
                }
                else
                {
                    placeArgus();
                }
                break;

            case ARStates.Dive:
                if (environmentIsPlaced)
                {
                    DestroyEnvironment();
                }
                else
                {
                    placeEnvironment();
                }
                break;
        }
    }

    public void placeHerc()
    {
        instantiatedHerc = Instantiate(hercGameObj, placementPose.position, placementPose.rotation);
        float value = getSliderValue(); 
        instantiatedHerc.transform.localScale = new Vector3(value, value, value) * 33.0f;
        hercIsPlaced = true;
    }

    public void placeArgus()
    {
        instantiatedArgus = Instantiate(argusGameObj, placementPose.position, placementPose.rotation);
        float value = getSliderValue();
        instantiatedArgus.transform.localScale = new Vector3(value, value, value);
        argusIsPlaced = true;
    }

    public void placeEnvironment()
    {
        placementPose.position = new Vector3(0, -1.0f,1.3f);
        placementPose.rotation = new Quaternion(0.0f, 0, 0, 0);
        instantiatedEnv = Instantiate(environment, placementPose.position, placementPose.rotation);
        instatiatedBlackCube = Instantiate(blackcube, placementPose.position, placementPose.rotation);
        environmentIsPlaced = true;
    }

    public void DestroyAllObjects()
    {
        //upon exiting the AR session, all vehicles are destroyed
        DestroyHerc();
        DestroyArgus();
        DestroyEnvironment();
    }

    public void DestroyHerc()
    {
        if (hercIsPlaced)
        {
            Destroy(instantiatedHerc);
            hercIsPlaced = false;
        }
    }

    public void DestroyArgus()
    {
        if (argusIsPlaced)
        {
            Destroy(instantiatedArgus);
            argusIsPlaced = false;
        }
    }

    public void DestroyEnvironment()
    {
        //CLIMATE CHANGE IS REAL
        if (environmentIsPlaced)
        {
            Destroy(instantiatedEnv);
            Destroy(instatiatedBlackCube);
            environmentIsPlaced = false;
        }
    }

    public void TransitionToHercARState()
    {
        currentARState = ARStates.Herc;
    }

    public void TransitionToArgusARState()
    {
        currentARState = ARStates.Argus;
    }

    public void TransitionToDiveARState()
    {
        currentARState = ARStates.Dive;
    }

    public void scaleVehicle(float value)
    {
        if (currentARState == ARStates.Herc && hercIsPlaced)
        {
            instantiatedHerc.transform.localScale = new Vector3(value, value, value)*33.0f; //Herc is scaled differently 

        } else if (currentARState == ARStates.Argus && argusIsPlaced)
        {
            instantiatedArgus.transform.localScale = new Vector3(value, value, value);
        }
    }

    public float getSliderValue()
    {
        return slider.GetComponent<Slider>().value;
    }

}
