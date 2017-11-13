using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleController : MonoBehaviour {

    public Transform target;

	// Use this for initialization
	void Start () {
        target = this.gameObject.transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(target.transform.position);
		
	}
}
