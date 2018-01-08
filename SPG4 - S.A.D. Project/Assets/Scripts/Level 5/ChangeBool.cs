using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeBool : MonoBehaviour {
    private bool clicked;
	// Use this for initialization
	void Start () {
        clicked = false;
	}


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if(clicked == false)
            {
                MoveToPointArray.puzzleDone = true;
                clicked = true;
            }
        }
    }

    //void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
    //    {
    //        MoveToPointArray.puzzleDone = false;
    //    }
    //}


    // Update is called once per frame
    void Update () {

	}
}
