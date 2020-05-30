using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]

public class DiveStartup : MonoBehaviour
{
    /// <summary>
    /// This controls the items in a Dive session to the coral reef.
    /// This involves all environment movies and sounds as well as instructions and GUI controls. 
    /// </summary>

    //game object connections
    public GameObject herc;
    public GameObject argus;
    public GameObject mainCamButton;
    public GameObject argusCamButton;
    public GameObject bottomCamButton;
    public GameObject slider;
    public GameObject volumeWarning;

    //public GameObject ScriptObject;
    public CameraScript cameraScript;
    public ARTapToPlaceObject arTapScript;
    public Material waterMaterial;
    public ParticleSystemForceField forcefield;
    public ParticleSystem upwardParticles;
    public ParticleSystem stationaryParticles;
    public ParticleSystem fog;
    public ParticleSystem homogeneousFog;
    //public ParticleSystem upwardFog;
    //private ParticleSystem.EmissionModule emisHeteroFog;
    //private ParticleSystem.EmissionModule emisHomoFog;

    //Timing
    private float START_DESCENT_TIME = 15; //55 is difference
    private float END_DESCENT_TIME = 60;
    private float TIME_SLIDER = 42;

    //initial conditions
    private bool descentSequenceStarted = false;
    private float diveStartTime;
    private Vector3 hercStartPosition;
    private Vector3 argusStartPosition;
    private float newExposure = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        //reset 
        hercStartPosition = herc.transform.position;
        argusStartPosition = argus.transform.position;

        //reset buttons to not be seen
        mainCamButton.SetActive(false);
        argusCamButton.SetActive(false);
        bottomCamButton.SetActive(false);
        slider.SetActive(false);
    }

    

    // Update is called once per frame
    void Update()
    {

        ///move herc in wave motion
        moveHercInWave();

        //remove volume warning
        if ((Time.time - diveStartTime) > 3)
        {
            volumeWarning.SetActive(false);
        }

        ////DESCENT
        if (descentSequenceStarted)
        {
            //CONTROL DESCENT MOVEMENT OF HERC
            if ((Time.time - diveStartTime) > START_DESCENT_TIME)
            { 
                //making skybox darker
                newExposure -= 0.05f * Time.deltaTime;
                RenderSettings.skybox.SetFloat("_Exposure", newExposure);

                //upward looking particles
                forcefield.gameObject.SetActive(true);
                upwardParticles.gameObject.SetActive(true);
                //upwardFog.gameObject.SetActive(true);

                if ((Time.time - diveStartTime) > START_DESCENT_TIME + 15)
                {
                    stationaryParticles.gameObject.SetActive(false);
                    var emisHeteroFog = fog.emission;
                    emisHeteroFog.rateOverTime = 0f;
                    var emisHomoFog = homogeneousFog.emission;
                    emisHomoFog.rateOverTime = 0f;
                }

                if ((Time.time - diveStartTime) > END_DESCENT_TIME)
                {
                    endDescent();
                }
            }

            //if ((Time.time - diveStartTime) > TIME_SLIDER)
            //{
            //    slider.SetActive(true);
            //}
        } 
    }

    public void moveHercInWave()
    {
        float xAmp = 75f;
        float xFreq = 0.5f;
        float yAmp = 200f;
        float yFreq = 0.6f;
        float zAmp = 50f;
        float zFreq = 0.7f;
        herc.transform.position = hercStartPosition + new Vector3(xAmp * Mathf.Sin(xFreq * Time.time), yAmp * Mathf.Sin(yFreq * Time.time), zAmp * Mathf.Sin(zFreq * Time.time));
    }

    public void startDiveSession()
    {
        descentSequenceStarted = true;
        diveStartTime = Time.time;
        resetHerc();
        resetFog();
        RenderSettings.skybox.SetFloat("_Exposure", 1.5f);
        checkVolume();

    }

    public void endDiveSession()
    {
    }

    public void endDescent()
    {
        descentSequenceStarted = false;
        
        //switch to AR
        cameraScript.SwitchToARCam();
        arTapScript.TransitionToDiveARState();
        arTapScript.DestroyOrPlaceObject();
    }

    public void resetHerc()
    {
        herc.transform.position = hercStartPosition;
        argus.transform.position = argusStartPosition;
    }

    public void resetFog()
    {
        fog.gameObject.SetActive(false);
        homogeneousFog.gameObject.SetActive(false);
        var emisHeteroFog = fog.emission;
        emisHeteroFog.rateOverTime = 500f;
        var emisHomoFog = homogeneousFog.emission;
        emisHomoFog.rateOverTime = 100f;

        newExposure = 1.5f;
        forcefield.gameObject.SetActive(false);
        upwardParticles.gameObject.SetActive(false);
        homogeneousFog.gameObject.SetActive(true);
        stationaryParticles.gameObject.SetActive(true);
        fog.gameObject.SetActive(true);
    }

    public void checkVolume()
    {
        if (volumedIsLow())
        {
            displayVolumeWarning();
        }
    }

    private bool volumedIsLow()
    {
        return true;
    }

    private void displayVolumeWarning()
    {
        volumeWarning.SetActive(true);
    }
}
