using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch2 : MonoBehaviour {
    public Camera lvl5CameraMain;
    public Camera lvl5Camera2;
    public Camera lvl5Camera3;
    public float lvl5Time;
    public float lvl5Time2;

	// Use this for initialization
	void Start () {
        
        lvl5CameraMain.enabled = false;
        lvl5Camera2.enabled = false;
        lvl5Camera3.enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        lvl5Time2 += Time.deltaTime;
        if (lvl5Time2>= 0.5f)
        {
            lvl5Camera3.enabled = false;
            lvl5Camera2.enabled = true;
        }
        lvl5Time += Time.deltaTime;
        if (lvl5Time>= 12.0f)
        {
            lvl5Camera2.enabled = false;
            lvl5CameraMain.enabled = true;
            lvl5Camera3.enabled = true;
        }
	}
}
