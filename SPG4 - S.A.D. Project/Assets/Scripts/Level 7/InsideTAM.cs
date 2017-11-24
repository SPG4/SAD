using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideTAM : MonoBehaviour {


    float time;
	// Use this for initialization
	void Start () {
        time = 40;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        time -= Time.deltaTime;
        if(time <= 0)
        {
            // transport players out of area. 
            Start();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
