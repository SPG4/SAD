using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class PuzzleCollider : MonoBehaviour {

    public AnalyticsTracker analyticsTracker;
    public int playerNr;
        
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerNr = 1;
            analyticsTracker.TriggerEvent();
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "Player2")
        {
            playerNr = 2;
            analyticsTracker.TriggerEvent();
            Destroy(gameObject);
        }

    }
  
}
