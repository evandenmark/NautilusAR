using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class ARStartup : MonoBehaviour
{
    /// <summary>
    /// This is the class that has to do with the Vehicle Augmented Reality.
    /// This only controls the GUI within that setting. 
    /// </summary>
    ///

    //GUI items
    public TextMeshProUGUI textObject;
    public GameObject hercButton;
    public GameObject argusButton;
    public GameObject slider;

    //timing and text instructions
    private static float increment = 3;
    private static float TIME_WELCOME = 0;
    private static float TIME_TAP = TIME_WELCOME + increment;
    private static float TIME_SLIDER = TIME_TAP + increment;
    private static float TIME_END = TIME_SLIDER + increment;

    private String TEXT_WELCOME = "Welcome to Nautilus Augmented Reality";
    private String TEXT_TAP = "Tap the vehicle you want to place";
    private String TEXT_SLIDER = "Scale the vehicle up to its full size";

    private float arStartTime;



    // Start is called before the first frame update
    void Start()
    {
        hercButton.SetActive(false);
        argusButton.SetActive(false);
        slider.SetActive(false);
        textObject.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.time - arStartTime) > TIME_WELCOME)
        {
            textObject.text = TEXT_WELCOME;
        }

        if ((Time.time - arStartTime) > TIME_TAP)
        {
            textObject.text = TEXT_TAP;
            hercButton.SetActive(true);
            argusButton.SetActive(true);
        }

        if ((Time.time - arStartTime) > TIME_SLIDER)
        {
            textObject.text = TEXT_SLIDER;
            slider.SetActive(true);
        }

        if ((Time.time - arStartTime) > TIME_END)
        {
            textObject.gameObject.SetActive(false);
        }

    }

    public void startAR()
    {
        //called every time a new AR session begins (every time the vehicle button is tapped)

        arStartTime = Time.time;
        hercButton.SetActive(false);
        argusButton.SetActive(false);
        slider.SetActive(false);
        textObject.gameObject.SetActive(false); //this resets the animation
        textObject.gameObject.SetActive(true);
    }


}
