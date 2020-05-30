using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newGyro : MonoBehaviour
{
    public GameObject frontCam;
    // Start is called before the first frame update
    private Quaternion _rawGyroRotation;
    void Start()
    {
        Input.gyro.enabled = true;

    }

    // Update is called once per frame
    void Update()
    {
        _rawGyroRotation = new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
        frontCam.transform.rotation = _rawGyroRotation;
        frontCam.transform.Rotate(90,0,90,Space.World);
    }
}
