using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanController : MonoBehaviour {

    public bool fanOn; 
    Transform particles;
    Transform wall;

    // Use this for initialization
    void Start () {
        particles = this.gameObject.transform.GetChild(0);
        wall = this.gameObject.transform.GetChild(1);
    }
	
	// Update is called once per frame
	void Update () {
        if (fanOn)
        {
            particles.gameObject.SetActive(true);
            wall.gameObject.SetActive(false);
        }
        else
        {
            particles.gameObject.SetActive(false);
            wall.gameObject.SetActive(true);
        }
	}

    private void TurnFanOn(bool turnFanOn)
    {
        if (turnFanOn)
            fanOn = true;
        else
            fanOn = false;
    }

    public bool IsFanOn()
    {
        return fanOn;
    }
}
