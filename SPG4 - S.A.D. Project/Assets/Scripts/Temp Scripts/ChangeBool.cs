using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBool : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        MoveToPointArray.puzzleDone = true;
    }

    // Update is called once per frame
    void Update () {

	}
}
