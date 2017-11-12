using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBool : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MoveToPointArray.puzzleDone = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            MoveToPointArray.puzzleDone = false;
        }
    }


    // Update is called once per frame
    void Update () {

	}
}
