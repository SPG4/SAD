using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PuzzleCollider : MonoBehaviour {

    public AnalyticsTracker analyticsTrackerP1, analyticsTrackerP2;
    // Use this for initialization
   
        
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1Collider")
        {
            analyticsTrackerP1.TriggerEvent();
        }

        else if (collision.gameObject.tag == "P2Collider")
        {
            analyticsTrackerP2.TriggerEvent();
        }

        Destroy(gameObject);
    }

     void Start () {
	}
	
    
	// Update is called once per frame
	void Update () {
	}
}
