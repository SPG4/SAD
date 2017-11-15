using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetRotationAngle : MonoBehaviour {

    public GameObject getAngleFrom;

    private Vector3 angle;
	// Use this for initialization
	void Start () {
        //angle = getAngleFrom.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        getAngleFrom.transform.Rotate(Vector3.forward);
    }
}
