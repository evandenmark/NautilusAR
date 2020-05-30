using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{

    public Light farLight1;
    public Light farLight2;
    public Light centerSpot1;
    public Light centerSpot2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeBrightness(float value)
    {
        centerSpot1.intensity = value;
        centerSpot2.intensity = value;
    }
}
