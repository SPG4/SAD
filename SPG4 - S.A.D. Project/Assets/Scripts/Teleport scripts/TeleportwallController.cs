using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportwallController : MonoBehaviour {

    private bool hasBeenUsed;

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ball"))
        {
            hasBeenUsed = true;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (hasBeenUsed)
        {
            this.gameObject.layer = LayerMask.NameToLayer("Default");
        }

        Debug.Log(hasBeenUsed);
	}
}
