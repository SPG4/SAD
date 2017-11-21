using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanButton : MonoBehaviour {

    FanController fan;

	// Use this for initialization
	void Start () {
        fan = gameObject.transform.parent.gameObject.GetComponentInChildren<FanController>();
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
            fan.SendMessage("TurnFanOn", true);
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
            fan.SendMessage("TurnFanOn", false);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
