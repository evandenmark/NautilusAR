using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public enum CameraStates { AR, Front, Bottom, Argus};
    public CameraStates currentCameraState;

    public GameObject ARCamera;
    public GameObject FrontCamera;
    public GameObject BottomCamera;
    public GameObject ArgusCamera;


    // Start is called before the first frame update
    void Awake()
    {
        currentCameraState = CameraStates.AR;

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentCameraState)
        {
            case CameraStates.AR:
                ARCamera.SetActive(true);
                FrontCamera.SetActive(false);
                BottomCamera.SetActive(false);
                ArgusCamera.SetActive(false);

                break;

            case CameraStates.Front:
                ARCamera.SetActive(false);
                FrontCamera.SetActive(true);
                BottomCamera.SetActive(false);
                ArgusCamera.SetActive(false);

                break;

            case CameraStates.Bottom:
                ARCamera.SetActive(false);
                FrontCamera.SetActive(false);
                BottomCamera.SetActive(true);
                ArgusCamera.SetActive(false);


                break;

            case CameraStates.Argus:
                ARCamera.SetActive(false);
                FrontCamera.SetActive(false);
                BottomCamera.SetActive(false);
                ArgusCamera.SetActive(true);

                break;


        }
    }

    public void SwitchToARCam()
    {
        currentCameraState = CameraStates.AR;
    }

    public void SwitchToFrontCam()
    {
        currentCameraState = CameraStates.Front;
    }

    public void SwitchToBottomCam()
    {
        currentCameraState = CameraStates.Bottom;
    }

    public void SwitchToArgusCam()
    {
        currentCameraState = CameraStates.Argus;
    }
}
