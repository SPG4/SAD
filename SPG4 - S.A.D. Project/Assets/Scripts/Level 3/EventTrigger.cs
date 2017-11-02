using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour {

    public GameObject blocker;
    bool hasHappened = false;

	void Start () {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasHappened)
        {
            GameObject blockerObject = Instantiate(blocker);
            hasHappened = true;
        }
        
    }
}
