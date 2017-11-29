using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanButton : MonoBehaviour {

    FanController fan;
    private GameObject eyeSprite;
    private GameObject closedEyeSprite;

    // Use this for initialization
    void Start () {
        fan = gameObject.transform.parent.gameObject.GetComponentInChildren<FanController>();
        eyeSprite = this.gameObject.transform.Find("EyeBtn_Small").gameObject;
        closedEyeSprite = this.gameObject.transform.Find("EyeBtn_Small_Closed").gameObject;

        eyeSprite.SetActive(false);
        closedEyeSprite.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        fan.SendMessage("TurnFanOn", true);
        eyeSprite.SetActive(true);
        closedEyeSprite.SetActive(false);
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
        fan.SendMessage("TurnFanOn", false);
        eyeSprite.SetActive(false);
        closedEyeSprite.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
