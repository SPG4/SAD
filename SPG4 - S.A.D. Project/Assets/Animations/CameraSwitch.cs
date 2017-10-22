using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour {

    public Camera cameraMain;
    public Camera camera2;
    public float time = 0.0f;
	// Use this for initialization
	void Start () {
        cameraMain.enabled = false;
        camera2.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

		    if (time >= 5.0f)
        {
            
            //camera.enabled = !camera.enabled;
            camera2.enabled = false; /* = !camera2.enabled;*/
            cameraMain.enabled = true;
        }
	}
}
