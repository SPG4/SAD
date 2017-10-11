// Maximilian Törn Almö
// 2017-10-03



//      *** Description ***
//
//
//
//


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEvent : MonoBehaviour {

    public string objectNameToMove = "";
    public float speedX = 0.0f;
    public float speedY = 0.0f;
    public float maxSeconds = 0.0f;
    public bool showDebug = false;

    private float currentSeconds = 0.0f;
	// Use this for initialization
	void Start () {

    }

    private void FixedUpdate()
    {

    }
    // Update is called once per frame
    void Update () {

	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(showDebug == true) { Debug.Log("Crate touching button"); }
        
        if (currentSeconds < maxSeconds){
            GameObject.Find(objectNameToMove).transform.Translate(new Vector2(speedX, speedY) * Time.deltaTime);
            maxSeconds -= Time.fixedDeltaTime;

            if (showDebug == true) { Debug.Log(maxSeconds); }
                
        }
        else
        {
            maxSeconds = 0;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }


}
